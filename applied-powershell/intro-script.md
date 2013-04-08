# Creating A Script

Let's find out what the weather is like.

Yahoo has an RSS based API that we can use at [http://developer.yahoo.com/weather/](http://developer.yahoo.com/weather/).

<..>

## Get-Weather.ps1
### Documentation Header

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

<aside class="notes" data-markdown>
  Comment headers don't just help for documentation, they are parsed by PowerShell
  when you use get-help or -?.
</aside>

<..>

## The Param declaration

<pre><code>param (
    [Parameter(Mandatory)]
    [string]
    $ZipCode,

    [switch]
    $InFahrenheit = $true,

    [switch]
    $InCelcius
)</code></pre>

<..>

## Let's call the API!

    # Invoke-RestMethod new in v3
    Invoke-RestMethod "http://weather.yahooapis.com/forecastrss?p=$ZipCode&u=$temperatureUnit"

<aside class="notes" data-markdown>
* http://weather.yahooapis.com/forecastrss?p=38002&u=F
  * pull it up in a browser to show what it looks like
* use Invoke-RestMethod (which knows about RSS) against it
* $result = Invoke-RestMethod "http://weather.yahooapis.com/forecastrss?p=38002&u=F" | select Title, Condition, Forecast
  * look at the $result instance
</aside>

<..>

## Building a container for our data

    # syntax is new in v3
    [PSCustomObject]@{
        Title = $results.title
        Condition = $results.condition.text
        Temperature = $results.condition.temp
        TodaysForecastTemp = "$($results.forecast[0].low)$tempUnit-$($results.forecast[0].high)$tempUnit"
        TodaysForecastCondition = $results.forecast[0].text
        TomorrowsForecastTemp = "$($results.forecast[1].low)$tempUnit-$($results.forecast[1].high)$tempUnit"
        TomorrowsForecastCondition = $results.forecast[1].text
    }

<..>

## Full Script

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
