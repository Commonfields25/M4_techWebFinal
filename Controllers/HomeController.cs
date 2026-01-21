using Microsoft.AspNetCore.Mvc;
using M4Webapp.Models;
using M4Webapp.Services;
using M4Webapp.Notifications;
using M4Webapp.Repositories;

namespace M4Webapp.Controllers;

public class HomeController : Controller
{
    private readonly IResourceService _resourceService;
    private readonly ISearchService _searchService;
    private readonly IValidationService _validationService;
    private readonly IEmailSender _emailSender;
    private readonly IMessageRepository _messageRepository;

    public HomeController(
        IResourceService resourceService,
        ISearchService searchService,
        IValidationService validationService,
        IEmailSender emailSender,
        IMessageRepository messageRepository)
    {
        _resourceService = resourceService;
        _searchService = searchService;
        _validationService = validationService;
        _emailSender = emailSender;
        _messageRepository = messageRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Resources(string? q, bool onlyFree = false, int? categoryId = null)
    {
        // Si aucun paramètre n'est passé (premier chargement), on active "Gratuit uniquement" par défaut
        if (Request.Query.Count == 0 && string.IsNullOrEmpty(q) && !categoryId.HasValue)
        {
            onlyFree = true;
        }

        var allResources = await _resourceService.GetAllAsync();
        var filteredResources = _searchService.Search(allResources, q, onlyFree, categoryId);

        ViewBag.CurrentQuery = q;
        ViewBag.OnlyFree = onlyFree;
        ViewBag.CurrentCategoryId = categoryId;

        // On récupère les catégories via le service ou directement via l'injection si besoin
        // Ici on utilise les données déjà chargées par simplicité, ou on pourrait ajouter une méthode GetCategoriesAsync au service
        ViewBag.Categories = allResources.Select(r => r.Category).DistinctBy(c => c.Id).OrderBy(c => c.Name).ToList();

        return View(filteredResources);
    }

    public IActionResult Contact()
    {
        return View(new ContactForm());
    }

    [HttpPost]
    public async Task<IActionResult> Contact(ContactForm form)
    {
        if (_validationService.TryValidate(form, out var results))
        {
            var message = new Message
            {
                Name = form.Name,
                Email = form.Email,
                Subject = form.Subject,
                Content = form.Message,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);
            await _emailSender.SendEmailAsync(form.Email, "Confirmation de contact", "Nous avons bien reçu votre message.");

            TempData["Success"] = "Votre message a été envoyé avec succès !";
            return RedirectToAction("Contact");
        }

        foreach (var error in results)
        {
            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? string.Empty, error.ErrorMessage ?? "Erreur");
        }

        return View(form);
    }
}
