using DemoSK.Planners.Plugins;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Reflection;

namespace DemoSK.Example.Plugins;

public class TimePlugin : PluginBase
{
    [KernelFunction, Description("Get the current date")]
    public string Date()
    {
        var r = DateTimeOffset.Now.ToString("D");

        Log(r, MethodBase.GetCurrentMethod()!.Name);

        return r;
    }
}
