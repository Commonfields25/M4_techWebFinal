using Microsoft.AspNetCore.Mvc;
using M4Webapp.Models;
using M4Webapp.Services;
using M4Webapp.Notifications;
using M4Webapp.Repositories;

namespace M4Webapp.Controllers;

/// <summary>
/// Contrôleur principal gérant l'affichage des pages d'accueil, des ressources et du formulaire de contact.
/// </summary>
public class HomeController : Controller
{
    private readonly IResourceService _resourceService;
    private readonly ISearchService _searchService;
    private readonly IValidationService _validationService;
    private readonly IEmailSender _emailSender;
    private readonly IMessageRepository _messageRepository;

    /// <summary>
    /// Initialise une nouvelle instance du contrôleur <see cref="HomeController"/>.
    /// </summary>
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

    /// <summary>
    /// Affiche la page d'accueil de l'application.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Affiche la liste des ressources éducatives avec options de recherche et filtrage.
    /// </summary>
    /// <param name="q">Chaîne de recherche pour filtrer les titres, descriptions ou thèmes.</param>
    /// <param name="onlyFree">Indique si seules les ressources gratuites doivent être affichées.</param>
    /// <param name="categoryId">Identifiant de la catégorie pour filtrer les ressources.</param>
    /// <returns>Une vue contenant la liste filtrée des ressources.</returns>
    public async Task<IActionResult> Resources(string? q, bool onlyFree = false, int? categoryId = null)
    {
        // Active le filtre "Gratuit uniquement" par défaut lors de la première visite
        if (Request.Query.Count == 0 && string.IsNullOrEmpty(q) && !categoryId.HasValue)
        {
            onlyFree = true;
        }

        // Récupération de toutes les ressources (avec chargement des entités liées)
        var allResources = await _resourceService.GetAllAsync();

        // Application de la logique de recherche et de filtrage
        var filteredResources = _searchService.Search(allResources, q, onlyFree, categoryId);

        // Préparation des données pour la vue
        ViewBag.CurrentQuery = q;
        ViewBag.OnlyFree = onlyFree;
        ViewBag.CurrentCategoryId = categoryId;

        // Extraction des catégories uniques pour le menu déroulant du filtre
        ViewBag.Categories = allResources.Select(r => r.Category).DistinctBy(c => c.Id).OrderBy(c => c.Name).ToList();

        return View(filteredResources);
    }

    /// <summary>
    /// Affiche la page de contact.
    /// </summary>
    public IActionResult Contact()
    {
        return View(new ContactForm());
    }

    /// <summary>
    /// Traite la soumission du formulaire de contact.
    /// </summary>
    /// <param name="form">Les données du formulaire soumises par l'utilisateur.</param>
    /// <returns>Redirige vers la page de contact en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
    [HttpPost]
    public async Task<IActionResult> Contact(ContactForm form)
    {
        // Validation manuelle via le service de validation
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

            // Sauvegarde du message dans la base de données
            await _messageRepository.AddAsync(message);

            // Simulation de l'envoi d'un email de confirmation
            await _emailSender.SendEmailAsync(form.Email, "Confirmation de contact", "Nous avons bien reçu votre message.");

            TempData["Success"] = "Votre message a été envoyé avec succès !";
            return RedirectToAction("Contact");
        }

        // Ajout des erreurs de validation à l'état du modèle pour affichage dans la vue
        foreach (var error in results)
        {
            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? string.Empty, error.ErrorMessage ?? "Erreur");
        }

        return View(form);
    }
}
