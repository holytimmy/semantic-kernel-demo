using DemoSK.Common;
using DemoSK.FunctionsCalling.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// get api key from user secrets
var openAICongig = ConfigHelper.OpenAI();

var _kernel = Kernel
            .CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: openAICongig.ModelId2,
                apiKey: openAICongig.ApiKey
            ).Build();

_kernel.ImportPluginFromType<TimePlugin>();
_kernel.ImportPluginFromPromptDirectory("Plugins");

var prompt = "Give me the current month name and encode 'myString' value into MY_FRM format.";

#region prompt
//var prompt = "Give me the name of the current month and encode it MY_FRM format.";
#endregion

var result1 = await _kernel.InvokePromptAsync(prompt);
Console.WriteLine("AutoInvoke --> [off]: \n" + result1.GetValue<string>());


Console.WriteLine("----------------------------------------------");


OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

var result2 = await _kernel.InvokePromptAsync(prompt, new(settings));
Console.WriteLine("AutoInvoke --> [on]: \n" + result2.GetValue<string>());

Console.ReadLine();