using BestStories.Shared.Models;

namespace BestStories.Shared.Clients.HackerNaws.Configuration;

public class HackerNewsCacheSettings : ISettings
{
    public static string KeyName => "HackerNewsCache";
    public int BestStoriesMinutes { get; init; } = 5;
    public int StoryMinutes { get; init; } = 10;
}

