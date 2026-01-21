using System.Text.Json;
using System.Text.Json.Serialization;
using M4Webapp.Models;

namespace M4Webapp.Data;

/// <summary>
/// Gère l'initialisation et le peuplement initial de la base de données.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Classe interne utilisée pour le transfert de données depuis le fichier JSON.
    /// </summary>
    private class ResourceDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsFree { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DifficultyLevel Difficulty { get; set; }
        public List<string> Topics { get; set; } = new();
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Initialise la base de données avec des données par défaut si celle-ci est vide.
    /// </summary>
    /// <param name="context">Le contexte de base de données à initialiser.</param>
    public static void Initialize(AppDbContext context)
    {
        // On ne peuple la base que si elle ne contient aucune ressource
        if (context.Resources.Any())
        {
            return;
        }

        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "resources.json");

        if (File.Exists(jsonPath))
        {
            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dtos = JsonSerializer.Deserialize<List<ResourceDto>>(json, options);

            if (dtos != null)
            {
                var categories = new Dictionary<string, Category>();
                var topics = new Dictionary<string, Topic>();

                foreach (var dto in dtos)
                {
                    // Gestion de la catégorie (Normalisation 3FN)
                    if (!categories.ContainsKey(dto.Category))
                    {
                        var cat = context.Categories.FirstOrDefault(c => c.Name == dto.Category)
                                 ?? new Category { Name = dto.Category };

                        if (cat.Id == 0) context.Categories.Add(cat);
                        categories[dto.Category] = cat;
                    }

                    // Gestion des thèmes (Relation Many-to-Many)
                    var resourceTopics = new List<Topic>();
                    foreach (var topicName in dto.Topics)
                    {
                        if (!topics.ContainsKey(topicName))
                        {
                            var t = context.Topics.FirstOrDefault(x => x.Name == topicName)
                                   ?? new Topic { Name = topicName };

                            if (t.Id == 0) context.Topics.Add(t);
                            topics[topicName] = t;
                        }
                        resourceTopics.Add(topics[topicName]);
                    }

                    // Création de l'entité Resource associée
                    var resource = new Resource
                    {
                        Title = dto.Title,
                        Description = dto.Description,
                        Category = categories[dto.Category],
                        Url = dto.Url,
                        Source = dto.Source,
                        ImageUrl = dto.ImageUrl,
                        IsFree = dto.IsFree,
                        Difficulty = dto.Difficulty,
                        Topics = resourceTopics,
                        Notes = dto.Notes,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Resources.Add(resource);
                }

                // Sauvegarde finale des changements dans SQLite
                context.SaveChanges();
            }
        }
    }
}
