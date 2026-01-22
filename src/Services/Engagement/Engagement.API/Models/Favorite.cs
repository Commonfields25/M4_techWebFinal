namespace Engagement.API.Models;
public class Favorite {
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ResourceId { get; set; }
}
