using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Handlebars;

namespace DemoSK.Planners;

public class Helper
{
    public async Task NoPlanner(Kernel kernel, string prompt)
    {
        LogMethod("NoPlanner");

        var result = await kernel.InvokePromptAsync(prompt!);
        LogResult(result.GetValue<string>()!);
    }

#pragma warning disable SKEXP0060
    public async Task Hadnlebars(Kernel kernel, string prompt)
    {
        LogMethod("Hadnlebars");


        var planner = new HandlebarsPlanner();

        var plan = await planner.CreatePlanAsync(kernel, prompt!);
        Console.WriteLine($"Expected plan => \n{plan}");

        var result = (await plan.InvokeAsync(kernel, [])).Trim();

        LogResult(result);
    }

    public async Task Stepwise(Kernel kernel, string prompt)
    {
        LogMethod("Stepwise");

        var options = new FunctionCallingStepwisePlannerOptions
        {
            MaxIterations = 15,
            MaxTokens = 4000,
        };
        var planner = new FunctionCallingStepwisePlanner(options);


        var plan = await planner.ExecuteAsync(kernel, prompt!);

        Console.WriteLine("\n\n-->Steps:");
        foreach (var msg in plan.ChatHistory!)
        {
            if (msg is OpenAIChatMessageContent)
            {
                var m = msg as OpenAIChatMessageContent;
                foreach (var item in m.ToolCalls)
                {
                    var s = item as ChatCompletionsFunctionToolCall;
                    Console.WriteLine($"-->[{msg.Role}] : {s.Name}");
                }
            }
            else
            {
                Console.WriteLine($"-->[{msg.Role}] : {msg.Content}");
            }
        }

        LogResult(plan.FinalAnswer);
    }
#pragma warning restore SKEXP0060

    private void LogMethod(string method)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"----------> {method} ");
        Console.ResetColor();
    }

    private void LogResult(string result)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Results => \n{result}");
        Console.ResetColor();
    }
}
