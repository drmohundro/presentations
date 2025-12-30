<#
.Synopsis
    Gets the weather by zipcode
.Description
    Calls the Yahoo Weather API to get the forecast for the specified zipcode and in the specified degrees.
.Notes 
    Author: David Mohundro
.Link
    http://mohundro.com
.Link
    http://developer.yahoo.com/weather/
.Parameter ZipCode
    The zipcode to get the weather for
#>

param (
    [Parameter(Mandatory)]
    [string] 
    $ZipCode,

    [switch]
    $InFahrenheit,

    [switch]
    $InCelcius
)

$tempUnit = if ($InFahrenheit) { "f" } elseif ($InCelcius) { "c" } else { "f" }

$results = Invoke-RestMethod "http://weather.yahooapis.com/forecastrss?p=$ZipCode&u=$tempUnit" |
    Select Title, Condition, Forecast

$tempUnit = $tempUnit.ToUpper()

[PSCustomObject]@{
    Title = $results.title
    Condition = $results.condition.text
    Temperature = $results.condition.temp
    TodaysForecastTemp = "$($results.forecast[0].low)$tempUnit-$($results.forecast[0].high)$tempUnit"
    TodaysForecastCondition = $results.forecast[0].text
    TomorrowsForecastTemp = "$($results.forecast[1].low)$tempUnit-$($results.forecast[1].high)$tempUnit"
    TomorrowsForecastCondition = $results.forecast[1].text
}