namespace DemoSK.Common;

public enum ModelType
{
    /// <summary>
    /// Updated legacy models (2023) -> provides the completion for a single prompt and takes a single string as an input
    /// </summary>
    TextGeneration,

    /// <summary>
    /// Newer models (2023–) -> provides the responses for a given dialog (with roles)
    /// </summary>
    ChatCompletion
}
