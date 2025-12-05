using BestStories.Features;
using BestStories.Shared.Clients.HackerNaws;
using BestStories.Shared.Clients.HackerNaws.Configuration;
using BestStories.Shared.Exceptions;
using BestStories.Shared.Extensions;
using MiWrap;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddOutputCache();

builder.Services.AddScoped<BestStoriesHandler>();

builder.ConfigureSettings<HackerNewsCacheSettings>();
builder.Services.AddHttpClient<IHackerNewsClient, HackerNewsClient>();
builder.Services.Decorate<IHackerNewsClient, CachedHackerNewsClient>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<InvalidServesResponseExceptionHandler>();
builder.Services.AddExceptionHandler<ServesUnavailableExceptionHandler>();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

builder.Services.AddExceptionHandler(options =>
{
    options.ExceptionHandlingPath = null;
    options.AllowStatusCode404Response = true;
});

var app = builder.Build();

app.UseOutputCache();

app.MapEndpoints<Program>();
app.MapGet("/health", () => "healthy!");

app.UseExceptionHandler();

app.Run();