namespace M4Webapp.Models;

/// <summary>
/// Représente une catégorie de ressources éducatives (ex: C#, ASP.NET Core, Azure).
/// </summary>
public class Category
{
    /// <summary>
    /// Identifiant unique de la catégorie.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de la catégorie.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Liste des ressources appartenant à cette catégorie.
    /// </summary>
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}
