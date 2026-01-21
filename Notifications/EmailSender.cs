namespace M4Webapp.Notifications;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        // Simulation d'envoi d'email
        _logger.LogInformation("SIMULATION EMAIL envoyé à {Email} avec sujet : {Subject}", email, subject);
        return Task.CompletedTask;
    }
}
