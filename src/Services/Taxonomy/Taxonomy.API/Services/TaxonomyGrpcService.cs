using Grpc.Core;
using M4Webapp.Shared.Protos.Taxonomy.v1;
using Microsoft.EntityFrameworkCore;
using Taxonomy.API.Data;

namespace Taxonomy.API.Services;

public class TaxonomyGrpcService : TaxonomyService.TaxonomyServiceBase
{
    private readonly TaxonomyDbContext _context;
    public TaxonomyGrpcService(TaxonomyDbContext context) { _context = context; }

    public override async Task<CategoriesResponse> GetCategories(Empty request, ServerCallContext context)
    {
        var categories = await _context.Categories.ToListAsync();
        var response = new CategoriesResponse();
        response.Categories.AddRange(categories.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name }));
        return response;
    }
}
