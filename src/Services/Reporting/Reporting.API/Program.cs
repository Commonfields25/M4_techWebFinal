using MassTransit;
using M4Webapp.Shared.Extensions;
using Reporting.API.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x => {
    x.AddConsumer<ResourceCreatedConsumer>();
    x.AddConsumer<UserRegisteredConsumer>();
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq", "/", h => {
            h.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
