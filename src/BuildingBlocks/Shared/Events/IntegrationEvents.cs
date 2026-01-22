namespace M4Webapp.Shared.Events;
public record ResourceCreatedEvent(int Id, string Title, string Category);
public record UserRegisteredEvent(string UserId, string Email);
public record ContactReceivedEvent(string Name, string Email, string Subject, string Content);
