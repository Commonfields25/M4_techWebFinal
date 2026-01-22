namespace M4Webapp.Models;

/// <summary>
/// Représente une ressource éducative normalisée (3FN).
/// </summary>
public class Resource
{
    /// <summary>
    /// Identifiant unique de la ressource.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre de la ressource.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description détaillée du contenu de la ressource.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant de la catégorie parente.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Référence vers l'entité Catégorie associée.
    /// </summary>
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Lien URL vers la ressource.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Origine de la ressource (ex: MicrosoftLearn, External).
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Chemin vers l'image ou le logo représentant la ressource.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Indique si la ressource est gratuite.
    /// </summary>
    public bool IsFree { get; set; }

    /// <summary>
    /// Niveau de difficulté de la ressource.
    /// </summary>
    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.All;

    /// <summary>
    /// Liste des thèmes (Topics) associés à cette ressource.
    /// </summary>
    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

    /// <summary>
    /// Notes additionnelles pour l'administration (ex: précisions sur le modèle freemium).
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Note moyenne donnée par les utilisateurs.
    /// </summary>
    public double AverageRating { get; set; }

    /// <summary>
    /// Nombre de notes reçues.
    /// </summary>
    public int RatingCount { get; set; }

    /// <summary>
    /// Date de création de l'entrée dans la base de données.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
