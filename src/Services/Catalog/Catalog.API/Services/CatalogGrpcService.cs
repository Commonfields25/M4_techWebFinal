using Grpc.Core;
using M4Webapp.Shared.Protos.Catalog.v1;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Data;

namespace Catalog.API.Services;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly CatalogDbContext _context;
    public CatalogGrpcService(CatalogDbContext context) { _context = context; }

    public override async Task<ResourceResponse> GetResourceById(GetResourceByIdRequest request, ServerCallContext context)
    {
        var resource = await _context.Resources.FindAsync(request.Id);
        if (resource == null) throw new RpcException(new Status(StatusCode.NotFound, "Resource not found"));
        return new ResourceResponse { Id = resource.Id, Title = resource.Title, Description = resource.Description };
    }
}
