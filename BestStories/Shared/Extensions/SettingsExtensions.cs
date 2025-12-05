using BestStories.Shared.Models;

namespace BestStories.Shared.Extensions;

public static class SettingsExtensions
{
    public static void ConfigureSettings<T>(this WebApplicationBuilder builder) where T : class, ISettings
    {
        var sectionName = builder.Configuration.GetSection(T.KeyName);
        builder.Services.Configure<T>(sectionName);
    }
}