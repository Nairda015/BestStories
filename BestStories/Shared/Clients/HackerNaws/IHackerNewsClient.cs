using BestStories.Shared.Clients.HackerNaws.Models;

namespace BestStories.Shared.Clients.HackerNaws;

public interface IHackerNewsClient
{
    Task<List<int>> GetBestStoriesIds();
    Task<Story> GetStory(int id);
}