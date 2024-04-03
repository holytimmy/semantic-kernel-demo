using Microsoft.Extensions.Configuration;

namespace DemoSK.Common;

public static class ConfigHelper
{
    public static OpenAI OpenAI()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<OpenAI>()
            .Build();

        return configuration
            .GetSection(nameof(OpenAI))
            .Get<OpenAI>()!;
    }

    public static GoogleConfig GoogleApi()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<GoogleConfig>()
            .Build();

        return configuration
            .GetSection(nameof(GoogleConfig))
            .Get<GoogleConfig>()!;
    }
}
