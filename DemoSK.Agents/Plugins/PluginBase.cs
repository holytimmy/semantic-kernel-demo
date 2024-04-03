using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoSK.Agents.Plugins;

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

    protected void Log(string result, string method, params object[] args)
    {
        string inputParams = "";
        foreach (var p in args)
        {
            inputParams += " [" + p.ToString() + "] ";
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{GetType().Name}.{method}  : {result}" + inputParams);
        Console.ResetColor();
    }
}
