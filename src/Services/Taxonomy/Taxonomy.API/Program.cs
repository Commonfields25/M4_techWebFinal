using Taxonomy.API.Data;
using Microsoft.EntityFrameworkCore;
using M4Webapp.Shared.Extensions;
using Taxonomy.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaxonomyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddCustomMassTransit(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.MapControllers();
app.MapGrpcService<TaxonomyGrpcService>();
app.Run();
