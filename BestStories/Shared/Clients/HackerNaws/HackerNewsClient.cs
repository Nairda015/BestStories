using BestStories.Shared.Clients.HackerNaws.Models;
using BestStories.Shared.Exceptions;

namespace BestStories.Shared.Clients.HackerNaws;

public class HackerNewsClient : IHackerNewsClient
{
    private readonly ILogger<HackerNewsClient> _logger;
    private readonly HttpClient _httpClient;
    
    public HackerNewsClient(HttpClient httpClient, ILogger<HackerNewsClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<int>> GetBestStoriesIds()
    {
        const string path = "https://hacker-news.firebaseio.com/v0/beststories.json";

        var response = await _httpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new ServesUnavailableException();
        
        var result = await response.Content.ReadFromJsonAsync<List<int>>();
        if (result is null) throw new InvalidServesResponseException();
        
        _logger.LogInformation("Hacker-news API returned {count} stories", result.Count);
        return result;
    }

    public async Task<Story> GetStory(int id)
    {
        var path = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";
        var response = await _httpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new ServesUnavailableException();
        
        var result = await response.Content.ReadFromJsonAsync<Story>();
        if (result is null) throw new InvalidServesResponseException();
        return result;
    }
}