using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace DemoSK.Common;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithConsoleLogging(this IKernelBuilder kernelBuilder, LogLevel level)
    {
        kernelBuilder.Services.AddLogging(c => c.AddConsole().SetMinimumLevel(level));

        return kernelBuilder;
    }

    public static IKernelBuilder WithOpenAI(this IKernelBuilder kernelBuilder, ModelType type)
    {
        var c = ConfigHelper.OpenAI();

        if (type == ModelType.TextGeneration)
        {
            kernelBuilder.AddOpenAITextGeneration(
                modelId: c.ModelId,
                apiKey: c.ApiKey
            );
        }
        else if (type == ModelType.ChatCompletion)
        {
            kernelBuilder.AddOpenAIChatCompletion(
                modelId: c.ModelId,
                apiKey: c.ApiKey
            );
        }
        return kernelBuilder;
    }

    public static IKernelBuilder WithTextToImage(this IKernelBuilder kernelBuilder)
    {
        var c = ConfigHelper.OpenAI();
#pragma warning disable SKEXP0010
        return kernelBuilder.AddOpenAITextToImage(c.ApiKey);
#pragma warning restore SKEXP0010
    }
}
