using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace DemoSK.Planners.Plugins;

public class WeatherPlugin : PluginBase
{
    [KernelFunction]
    [Description("Get the weather forecast for the given date and location.")]
    public string GetForecast(
            [Description("Date on which the weather forecast will be provided")]
            string date,

            [Description("Location for which the weather forecast is provided")]
            string location
        )
    {
        var listings = LookupForecast(date, location);

        var r = JsonSerializer.Serialize(listings, _options);
        Log(r, MethodBase.GetCurrentMethod()!.Name);

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
