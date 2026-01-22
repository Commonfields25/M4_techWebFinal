using MassTransit;
using M4Webapp.Shared.Events;

namespace Notification.API.Consumers;

public class ContactReceivedConsumer : IConsumer<ContactReceivedEvent>
{
    private readonly ILogger<ContactReceivedConsumer> _logger;
    public ContactReceivedConsumer(ILogger<ContactReceivedConsumer> logger) { _logger = logger; }

    public Task Consume(ConsumeContext<ContactReceivedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Sending email notification for contact from {Name} ({Email}): {Subject}", message.Name, message.Email, message.Subject);
        // Here we would call an EmailService
        return Task.CompletedTask;
    }
}
