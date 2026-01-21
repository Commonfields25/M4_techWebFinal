using System.ComponentModel.DataAnnotations;

namespace M4Webapp.Models;

/// <summary>
/// Définit les niveaux de difficulté possibles pour une ressource.
/// </summary>
public enum DifficultyLevel
{
    /// <summary>Niveau débutant.</summary>
    Beginner,
    /// <summary>Niveau intermédiaire.</summary>
    Intermediate,
    /// <summary>Niveau avancé.</summary>
    Advanced,
    /// <summary>Tous niveaux.</summary>
    All
}

/// <summary>
/// Modèle représentant un message reçu via le formulaire de contact.
/// </summary>
public class Message
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Modèle de vue pour le formulaire de contact avec annotations de validation.
/// </summary>
public class ContactForm
{
    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le sujet est obligatoire.")]
    [StringLength(200, ErrorMessage = "Le sujet ne peut pas dépasser 200 caractères.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le message est obligatoire.")]
    [MinLength(10, ErrorMessage = "Le message doit faire au moins 10 caractères.")]
    public string Message { get; set; } = string.Empty;
}
