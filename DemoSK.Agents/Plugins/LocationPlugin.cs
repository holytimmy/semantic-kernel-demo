using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace DemoSK.Agents.Plugins;

public class LocationPlugin : PluginBase
{
    [KernelFunction]
    [Description("Get location of the user based on their name")]
    public string GetCurrentLocation(
        [Description("User name")] string userName)
    {
        var location = new Location(
            Zip: "00-012",
            City: "Warsaw",
            Country: "Poland"
            );

        var r = JsonSerializer.Serialize(location, _options);
        Log(r, MethodBase.GetCurrentMethod()!.Name, userName);

        return r;
    }

    #region private
    private record Location(
        string Zip,
        string City,
        string Country);
    #endregion

}
