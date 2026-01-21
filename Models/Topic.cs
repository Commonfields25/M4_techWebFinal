namespace M4Webapp.Models;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Relation Many-to-Many
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}
