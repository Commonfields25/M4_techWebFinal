namespace Catalog.API.Models;
public class Resource {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsFree { get; set; }
    public string Difficulty { get; set; } = "Beginner";
}
