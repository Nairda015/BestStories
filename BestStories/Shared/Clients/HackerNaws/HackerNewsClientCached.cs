using BestStories.Shared.Clients.HackerNaws.Configuration;
using BestStories.Shared.Clients.HackerNaws.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BestStories.Shared.Clients.HackerNaws;

public class CachedHackerNewsClient : IHackerNewsClient
{
    private readonly IHackerNewsClient _decorated;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedHackerNewsClient> _logger;
    private readonly HackerNewsCacheSettings _settings;

    public CachedHackerNewsClient(
        IHackerNewsClient decorated,
        IMemoryCache cache,
        IOptions<HackerNewsCacheSettings> options,
        ILogger<CachedHackerNewsClient> logger)
    {
        _decorated = decorated;
        _cache = cache;
        _logger = logger;
        _settings = options.Value;
    }

    public async Task<List<int>> GetBestStoriesIds() => await _cache.GetOrCreateAsync("best_stories_ids", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.BestStoriesMinutes);

        _logger.LogInformation("Cache miss for best stories.");

        return await _decorated.GetBestStoriesIds();
    });

    public async Task<Story> GetStory(int id) => await _cache.GetOrCreateAsync($"story_{id}", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.StoryMinutes);

        _logger.LogInformation("Cache miss for story {id}.", id);

        return await _decorated.GetStory(id);
    });
}