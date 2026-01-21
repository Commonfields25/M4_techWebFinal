using M4Webapp.Models;

namespace M4Webapp.Services;

/// <summary>
/// Définit les opérations de recherche et de filtrage sur les ressources.
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Filtre et ordonne une collection de ressources selon plusieurs critères.
    /// </summary>
    /// <param name="resources">La collection de ressources à filtrer.</param>
    /// <param name="query">Terme de recherche (optionnel).</param>
    /// <param name="onlyFree">Si vrai, exclut les ressources payantes.</param>
    /// <param name="categoryId">Filtre par identifiant de catégorie (optionnel).</param>
    /// <returns>Une collection de ressources répondant aux critères, triée par gratuité puis par titre.</returns>
    IEnumerable<Resource> Search(IEnumerable<Resource> resources, string? query, bool onlyFree = true, int? categoryId = null);
}

/// <summary>
/// Implémentation du service de recherche respectant le principe "Free-First".
/// </summary>
public class SearchService : ISearchService
{
    public IEnumerable<Resource> Search(IEnumerable<Resource> resources, string? query, bool onlyFree = true, int? categoryId = null)
    {
        var result = resources.AsQueryable();

        // Application du filtre de gratuité
        if (onlyFree)
        {
            result = result.Where(r => r.IsFree);
        }

        // Application du filtre de catégorie
        if (categoryId.HasValue && categoryId.Value > 0)
        {
            result = result.Where(r => r.CategoryId == categoryId.Value);
        }

        // Application du filtre textuel
        if (!string.IsNullOrEmpty(query))
        {
            query = query.ToLower();
            result = result.Where(r =>
                r.Title.ToLower().Contains(query) ||
                r.Description.ToLower().Contains(query) ||
                r.Topics.Any(t => t.Name.ToLower().Contains(query))
            );
        }

        // Tri : Les ressources gratuites apparaissent en premier (si le filtre n'est pas activé),
        // puis tri par ordre alphabétique du titre.
        return result.OrderByDescending(r => r.IsFree).ThenBy(r => r.Title).ToList();
    }
}
