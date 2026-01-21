using System.Text.Json;
using System.Text.Json.Serialization;
using M4Webapp.Models;

namespace M4Webapp.Data;

public static class SeedData
{
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

    public static void Initialize(AppDbContext context)
    {
        if (context.Resources.Any())
        {
            return;
        }

        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "resources.json");
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
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // Note: JsonStringEnumConverter can also be added here or via attribute
            var dtos = JsonSerializer.Deserialize<List<ResourceDto>>(json, options);

            if (dtos != null)
            {
                var categories = new Dictionary<string, Category>();
                var topics = new Dictionary<string, Topic>();

                foreach (var dto in dtos)
                {
                    if (!categories.ContainsKey(dto.Category))
                    {
                        var cat = context.Categories.FirstOrDefault(c => c.Name == dto.Category)
                                 ?? new Category { Name = dto.Category };

                        if (cat.Id == 0) context.Categories.Add(cat);
                        categories[dto.Category] = cat;
                    }

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
            var resources = JsonSerializer.Deserialize<List<Resource>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (resources != null)
            {
                context.Resources.AddRange(resources);
                context.SaveChanges();
            }
        }
    }
}
