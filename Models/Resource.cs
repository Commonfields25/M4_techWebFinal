namespace M4Webapp.Models;

public enum DifficultyLevel
{
    Beginner,
    Intermediate,
    Advanced,
    All
}

public class Resource
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsFree { get; set; }
    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.All;
    public List<string> Topics { get; set; } = new();
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
