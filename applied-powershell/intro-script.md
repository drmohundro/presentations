# Creating A Script

<..>

## Before we get started
### The top four cmdlets to remember...

        Get-Help
        # alias is man
        # can also use by typing in a command and then -?

        Get-Command
        # alias is gcm
        # sort of like `which` in UNIX

        Get-Member
        # alias is gm
        # usually used by piping something into `Get-Member`

        Get-PSDrive
        # get currently mounted PSDrives
        # i.e. what can I "cd" into


<aside class="notes" data-markdown>
It would be good to demo all of these if time allows... *especially* get-command and get-member
* Get-Command (useful to see *what* all is available to call)
    * go ahead and run `get-command` to show all available commands
* Get-Member (useful to see *what* all is available to call *on an object*)
    * go ahead and run `dir | gm` to show everything is .NET
</aside>
<..>

## Scenario

Let's find out what the weather is like.

Yahoo has an RSS based API that we can use at [http://developer.yahoo.com/weather/](http://developer.yahoo.com/weather/).

<..>

## Get-Weather.ps1
Let's create the file and add some basic documentation

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

We've got documentation... how do we want to call this script? Maybe `Get-Weather -zipCode 12345 -inFahrenheit`? 

We can default to Fahrenheit.

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

<span class="fragment">
But... what now? We *could* pull in [System.ServiceModel.Syndication](http://msdn.microsoft.com/en-us/library/system.servicemodel.syndication.aspx) to parse the RSS, but that seems... less than ideal.
</span>

<span class="fragment">
Let's try finding something useful with `Get-Command`
</span>

<pre class="fragment"><code># Invoke-RestMethod new in v3
Invoke-RestMethod "http://weather.yahooapis.com/forecastrss?p=$ZipCode&u=$temperatureUnit"
</code></pre>

<aside class="notes" data-markdown>
* http://weather.yahooapis.com/forecastrss?p=38002&u=F
  * pull it up in a browser to show what it looks like

<pre>
gcm *web*
gcm *web* | where modulename -ne 'WebAdministration'
gcm *web* -commandtype cmdlet | where modulename -ne 'WebAdministration'
# Hmm... Invoke-WebRequests looks promising.
get-help invoke-webrequest
# check out the related links... Invoke-RestMethod
get-help invoke-resetmethod -full
# check out the RSS example... RIGHT THERE!!!
</pre>

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
