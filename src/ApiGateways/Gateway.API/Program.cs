using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configuration already reads appsettings.json and appsettings.{env}.json by default.
// If you want additional config sources, add them here:
// builder.Configuration.AddEnvironmentVariables();

// Services
builder.Services.AddHealthChecks();

// Reverse proxy from config
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    // Example: add a transform factory globally (optional)
    .AddTransforms(builderContext =>
    {
        // Add X-Forwarded-For / X-Forwarded-Proto if needed
        builderContext.AddRequestTransform(context =>
        {
            context.ProxyRequest.Headers.Remove("X-Forwarded-Host");
            return ValueTask.CompletedTask;
        });
    });

// Optional: JWT bearer authentication example (enable/comment as needed)
// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options =>
//     {
//         options.Authority = builder.Configuration["Authentication:Authority"];
//         options.Audience = builder.Configuration["Authentication:Audience"];
//         options.RequireHttpsMetadata = false; // set to true in production
//     });

// Forwarded headers (when running behind a load balancer / k8s)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                               Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
    // options.KnownProxies / options.KnownNetworks can be set for stricter security
});

var app = builder.Build();

// Use forwarded headers early in pipeline
app.UseForwardedHeaders();

// Use authentication/authorization if enabled
// app.UseAuthentication();
// app.UseAuthorization();

// Health endpoints
app.MapHealthChecks("/health");
app.MapGet("/ready", async context =>
{
    // Add readiness checks if needed
    await context.Response.WriteAsync("Ready");
});

// Map YARP reverse proxy
app.MapReverseProxy();

// Graceful shutdown logging
var lifetime = app.Lifetime;
lifetime.ApplicationStarted.Register(() => app.Logger.LogInformation("Gateway started."));
lifetime.ApplicationStopping.Register(() => app.Logger.LogInformation("Gateway stopping..."));
lifetime.ApplicationStopped.Register(() => app.Logger.LogInformation("Gateway stopped."));

app.Run();
