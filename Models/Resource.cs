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

    // Relation vers Category (3FN)
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsFree { get; set; }
    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.All;

    // Relation Many-to-Many vers Topic (3FN)
    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
