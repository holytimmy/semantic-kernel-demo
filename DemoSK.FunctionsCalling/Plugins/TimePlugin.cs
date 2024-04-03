using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DemoSK.FunctionsCalling.Plugins;

internal class TimePlugin
{
    [KernelFunction, Description("Get the current month")]
    public string Month()
    {
        var result = DateTimeOffset.Now.ToString("MMMM");
        Log("TimePlugin.Month", result);
        return result;
    }

    [KernelFunction, Description("Get the current time")]
    public string Time()
    {
        var result = DateTimeOffset.Now.ToString("hh:mm:ss tt");
        Log("TimePlugin.Time", result);
        return result;
    }

    private void Log(string method, string result)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Calling '{method}' function. Result: '{result}'");
        Console.ResetColor();
    }
}
