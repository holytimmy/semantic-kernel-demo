using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace DemoSK.Agents.Plugins;

public class WeatherPlugin : PluginBase
{
    [KernelFunction, Description("Get the forecast for the specified date at the specified location.")]
    public string GetForecast(
        [Description("Date")] string date,
        [Description("Location name")] string location
        )
    {
        var listings = LookupForecast(date, location);

        var r = JsonSerializer.Serialize(listings, _options);
        Log(r, MethodBase.GetCurrentMethod()!.Name, date, location);

        return r;
    }

    #region private helpers 
    private WeatherForecast LookupForecast(string date, string location)
    {
        var temp = Random.Shared.Next(-15, 25);

        var conditionValues = Enum.GetValues<WeatherConditions>();
        var condition = conditionValues[Random.Shared.Next(conditionValues.Length)];

        return
            new WeatherForecast(
                date,
                location,
                condition,
                $"{temp} C");
    }

    private enum WeatherConditions
    {
        Sunny,
        Cloudy,
        Rainy,
        Snowy,
        Foggy,
        Windy,
        Stormy,
        Hazy,
        Overcast,
        Thunderstorms
    }

    private record WeatherForecast(
        string Date,
        string Locations,
        WeatherConditions WeatherCondition,
        string Temperature);

    #endregion
}
