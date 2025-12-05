using BestStories.Shared.Clients.HackerNaws;
using BestStories.Shared.Clients.HackerNaws.Models;
using MiWrap;

namespace BestStories.Features;

internal record BestStories(int NumberOfStories) : IHttpQuery;

public class BestStoriesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        builder.MapGet<BestStories, BestStoriesHandler>("stories/{numberOfStories}")
            .Produces<List<Story>>()
            .Produces(500)
            .CacheOutput();
}

internal class BestStoriesHandler : IHttpQueryHandler<BestStories>
{
    private readonly IHackerNewsClient _client;
    private readonly ILogger<BestStoriesEndpoint> _logger;

    public BestStoriesHandler(IHackerNewsClient client, ILogger<BestStoriesEndpoint> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IResult> HandleAsync(BestStories query, CancellationToken cancellationToken)
    {
        _logger.LogDebug("BestStoriesHandler start");
        var bestStoriesIds = await _client.GetBestStoriesIds();

        var storyTasks = bestStoriesIds
            .Take(query.NumberOfStories)
            .Select(id => _client.GetStory(id));

        var stories = await Task.WhenAll(storyTasks);
        var ordered = stories.OrderByDescending(x => x.Score).ToList();

        _logger.LogDebug("BestStoriesHandler end");
        return Results.Ok(ordered);
    }
}