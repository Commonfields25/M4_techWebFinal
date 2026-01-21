using System.Text.Json;
using M4Webapp.Models;

namespace M4Webapp.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Resources.Any())
        {
            return; // Déjà rempli
        }

        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "resources.json");
        // En mode dev, on peut aussi chercher directement dans le dossier du projet
        if (!File.Exists(jsonPath))
        {
            jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "resources.json");
        }

        if (File.Exists(jsonPath))
        {
            var json = File.ReadAllText(jsonPath);
            var resources = JsonSerializer.Deserialize<List<Resource>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (resources != null)
            {
                context.Resources.AddRange(resources);
                context.SaveChanges();
            }
        }
    }
}
