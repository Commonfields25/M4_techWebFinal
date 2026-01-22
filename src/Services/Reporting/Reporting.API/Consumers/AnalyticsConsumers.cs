using MassTransit;
using M4Webapp.Shared.Events;

namespace Reporting.API.Consumers;

public class ResourceCreatedConsumer : IConsumer<ResourceCreatedEvent> {
    public Task Consume(ConsumeContext<ResourceCreatedEvent> context) { return Task.CompletedTask; }
}

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent> {
    public Task Consume(ConsumeContext<UserRegisteredEvent> context) { return Task.CompletedTask; }
}
