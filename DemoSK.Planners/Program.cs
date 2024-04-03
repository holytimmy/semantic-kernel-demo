using DemoSK.Common;
using DemoSK.Example.Plugins;
using DemoSK.Planners;
using DemoSK.Planners.Plugins;
using Microsoft.SemanticKernel;

// get api key from user secrets
var openAICongig = ConfigHelper.OpenAI();

var kernel = Kernel
            .CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: openAICongig.ModelId2,
                apiKey: openAICongig.ApiKey)
            .Build();

// native
kernel.ImportPluginFromType<TimePlugin>();
kernel.ImportPluginFromType<WeatherPlugin>();
// semantic
kernel.ImportPluginFromPromptDirectory("Plugins");


var h = new Helper();
var request = "What is the weather forecast in London? Answer in the form of a poem.";

await h.NoPlanner(kernel, request);
await h.Hadnlebars(kernel, request);
await h.Stepwise(kernel, request);


Console.ReadLine();
