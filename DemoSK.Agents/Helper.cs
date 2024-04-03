using DemoSK.Agents.Plugins;
using DemoSK.Common;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Agents;

namespace DemoSK.Agents;

internal static class Helper
{
    private static readonly Dictionary<string, IAgent> agents = [];

    public static IAgent GetTimeAgent()
    {
        var name = "TimeAgent";
        var openAICongig = ConfigHelper.OpenAI();
        var plugin = KernelPluginFactory.CreateFromType<TimePlugin>();

        return agents[name] =

            new AgentBuilder()
                .WithOpenAIChatCompletion(openAICongig.ModelId, openAICongig.ApiKey)
                .WithName(name)
                .WithInstructions(
                    "You are able to provide current date. Do not produce any data. " +
                    "Use only the available functions. Return result in JSON format.")
                .WithDescription("Returns the current date.")
                .WithPlugin(plugin)
                .BuildAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }

    public static IAgent GetWeatherAgent()
    {
        var name = "WeatherAgent";
        var openAICongig = ConfigHelper.OpenAI();
        var plugin = KernelPluginFactory.CreateFromType<WeatherPlugin>();

        return agents[name] =
            new AgentBuilder()
                .WithOpenAIChatCompletion(openAICongig.ModelId, openAICongig.ApiKey)
                .WithName(name)
                .WithInstructions(
                    "You are able to provide weather forecast. " +
                    "Do not produce any data. " +
                    "Use only the provided functions to return the weather forecast. " +
                    "Return result in JSON format.")
                .WithDescription("Returns weather forecast based on provided location and date.")
                .WithPlugin(plugin)
                .BuildAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }

    public static IAgent GetLocationAgent()
    {
        var name = "LocationAgent";
        var openAICongig = ConfigHelper.OpenAI();
        
        var plugin = KernelPluginFactory.CreateFromType<LocationPlugin>();

        return agents[name] =
            new AgentBuilder()
                .WithOpenAIChatCompletion(openAICongig.ModelId, openAICongig.ApiKey)
                .WithName(name)
                .WithCodeInterpreter()
                .WithInstructions(
                    "You are able to provide user location. " +
                    "Do not produce any values. Use only the provided functions. " +
                    "Return result in JSON format.")
                .WithDescription("Provides user location information based on user name.")
                .WithPlugin(plugin)
                .BuildAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }

    public static IAgent GetMainAgent(params KernelPlugin[] plugins)
    {
        var name = "MainAgent";
        var openAICongig = ConfigHelper.OpenAI();

        return agents[name] =
            new AgentBuilder()
                .WithOpenAIChatCompletion(openAICongig.ModelId, openAICongig.ApiKey)
                .WithName(name)
                .WithInstructions(
"""
You're a wicked pirate. Use pirate jargon and pirate jokes in every answer.

Respond to user inquiries by working only with the agents provided, 
giving them complete and accurate instructions.
""")
                .WithPlugins(plugins)
                .BuildAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }

    public static async Task DeleteAgentsAsync()
    {
        foreach (var agent in agents.Values)
        {
            try
            {
                await agent.DeleteAsync();
            }
            catch
            {
                Console.WriteLine($"!!! FAILURE - Deleting agent {agent.Id} ({agent.Name})");
            }
        }

        agents.Clear();
    }
}
