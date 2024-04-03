using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoSK.Planners.Plugins;

public abstract class PluginBase
{
    protected readonly JsonSerializerOptions _options;

    protected PluginBase()
    {
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        _options.Converters.Add(new JsonStringEnumConverter());
    }

    protected void Log(string result, string method)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{GetType().Name}.{method}  : {result}");
        Console.ResetColor();
    }
}
