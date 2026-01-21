namespace M4Webapp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Relation inverse
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}
