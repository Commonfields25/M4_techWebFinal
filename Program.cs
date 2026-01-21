using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure SQLite DbContext
var connectionString = builder.Configuration.GetConnectionString("SqliteConnection") ?? "Data Source=./data/m4webapp.db";
builder.Services.AddDbContext<M4Webapp.Data.AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Message repository (file-based) - keep for backward compatibility
builder.Services.AddSingleton<M4Webapp.Repositories.IMessageRepository, M4Webapp.Repositories.FileMessageRepository>();

// Resource service - DB-backed implementation
builder.Services.AddScoped<M4Webapp.Services.IResourceService, M4Webapp.Services.DbResourceService>();

// Search service
builder.Services.AddScoped<M4Webapp.Services.ISearchService, M4Webapp.Services.SearchService>();

// Theme service
builder.Services.AddScoped<M4Webapp.Services.IThemeService, M4Webapp.Services.ThemeService>();

// Validation service
builder.Services.AddScoped<M4Webapp.Services.IValidationService, M4Webapp.Services.ValidationService>();

// Email sender
builder.Services.AddScoped<M4Webapp.Notifications.IEmailSender, M4Webapp.Notifications.EmailSender>();

var app = builder.Build();

// Seed database (ensure migrations applied externally)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<M4Webapp.Data.AppDbContext>();
        // Apply any pending migrations automatically
        context.Database.Migrate();

        M4Webapp.Data.SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

