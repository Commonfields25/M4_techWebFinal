using M4Webapp.Models;

namespace M4Webapp.Services;

public interface ISearchService
{
    IEnumerable<Resource> Search(IEnumerable<Resource> resources, string? query, bool onlyFree = true, int? categoryId = null);
}

public class SearchService : ISearchService
{
    public IEnumerable<Resource> Search(IEnumerable<Resource> resources, string? query, bool onlyFree = true, int? categoryId = null)
    {
        var result = resources.AsQueryable();

        if (onlyFree)
        {
            result = result.Where(r => r.IsFree);
        }

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            result = result.Where(r => r.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrEmpty(query))
        {
            query = query.ToLower();
            result = result.Where(r =>
                r.Title.ToLower().Contains(query) ||
                r.Description.ToLower().Contains(query) ||
                r.Topics.Any(t => t.Name.ToLower().Contains(query))
            );
        }

        return result.OrderByDescending(r => r.IsFree).ThenBy(r => r.Title).ToList();
    }
}
