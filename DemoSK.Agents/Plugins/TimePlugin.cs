using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace DemoSK.Agents.Plugins;

public class TimePlugin : PluginBase
{
    [KernelFunction, Description("Get the current date")]
    public string Date(IFormatProvider? formatProvider = null)
    {
        var d = DateTimeOffset.Now.ToString("D", formatProvider);

        var r = JsonSerializer.Serialize(d, _options);
        Log(r, MethodBase.GetCurrentMethod()!.Name);

        return r;
    }

    [KernelFunction, Description("Get the current date")]
    public string Today(IFormatProvider? formatProvider = null)
    {
        return Date(formatProvider);
    }

    [KernelFunction, Description("Add the number of days to current date")]
    public string Add(
        [Description("Number of days")] int numberOfDays,
        IFormatProvider? formatProvider = null)
    {
        var d = DateTimeOffset.Now.AddDays(numberOfDays).ToString("D", formatProvider);

        var r = JsonSerializer.Serialize(d, _options);
        Log(r, MethodBase.GetCurrentMethod()!.Name, numberOfDays);

        return r;
    }
}
