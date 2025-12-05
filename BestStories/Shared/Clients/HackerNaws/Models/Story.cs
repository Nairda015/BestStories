namespace BestStories.Shared.Clients.HackerNaws.Models;

public record Story(
    string By,
    int Descendants,
    int Id,
    int[] Kids,
    int Score,
    int Time,
    string Title,
    string Type,
    string Url
);