using M4Webapp.Models;

namespace M4Webapp.Repositories;

/// <summary>
/// Contrat pour l'accès aux messages.
/// Toutes les opérations sont asynchrones.
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Ajoute un message et le persiste en base.
    /// </summary>
    /// <param name="message">Le message à ajouter (ne doit pas être null).</param>
    Task AddAsync(Message message);

    /// <summary>
    /// Récupère tous les messages existants.
    /// </summary>
    /// <returns>Une collection de <see cref="Message"/>.</returns>
    Task<IEnumerable<Message>> GetAllAsync();
}

