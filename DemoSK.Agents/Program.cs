using DemoSK.Agents;
using DemoSK.Common;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

var openAICongig = ConfigHelper.OpenAI();
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(openAICongig.ModelId, openAICongig.ApiKey)
    .WithConsoleLogging(LogLevel.Trace)
    .Build();

try
{
    var mainAgent = Helper.GetMainAgent(
        Helper.GetTimeAgent().AsPlugin(),
        Helper.GetWeatherAgent().AsPlugin(),
        Helper.GetLocationAgent().AsPlugin()
    );

    var thread = await mainAgent.NewThreadAsync();

    // Hi, my name is Mike. What is the weather forecast for tomorrow?
    // What is the weather expected to be the day after tomorrow?
    // What will the weather be like in 14 days?

    while (true)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write("User --> ");
        Console.ResetColor();

        string request = Console.ReadLine()!;

        if (request == "-e")
        {
            break;
        }

        // Add user request to history
        await thread.AddUserMessageAsync(request);

        var agentMessages = await thread.InvokeAsync(mainAgent).ToArrayAsync();
        foreach (var message in agentMessages)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write($"{message.Role} ({mainAgent.Name}) --> ");
            Console.ResetColor();

            Console.WriteLine(message.Content);
        }
    }
}
finally
{
    await Helper.DeleteAgentsAsync();
}
