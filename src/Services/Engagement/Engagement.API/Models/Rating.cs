namespace Engagement.API.Models;
public class Rating {
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ResourceId { get; set; }
    public int Value { get; set; }
}
