# Syntax Overview

<..>

## Variables

    # types aren't required...
    $aStringVar = 'hello there'

    # but they can be used
    [string]$anotherStr = 'hi again'
    $anotherStr = 5   # allowed because numbers can become strings

    # notice type inference from strings to numbers (like above)
    [int]$intVar = 5
    $intVar = '55'
    $intVar = 'hello'   # error because this can't become a number

    # validatable types (new in version 3!)
    [ValidateRange(0,10)][int]$rangedInt = 1
    $rangedInt = 11    # ERROR!

<aside class="notes" data-markdown>
  variables are prefixed with a $... feels like Perl huh?
</aside>

<..>

## Variables continued

    # arrays
    $arr = 1,2,3    # or (1,2,3)

    # objects... remember, they're just .NET objects
    $wc = New-Object System.Net.WebClient
    $wc.DownloadString('http://www.google.com')

    # pulling your own types in
    Add-Type -path path/to/your/assembly
    $yourType = New-Object My.Awesome.Type

<aside class="notes" data-markdown>
  * loading your own types is *especially* useful if you are writing a testing script
  * *almost* a mini unit testing tool
  * certainly quicker than writing a full blown console app, compiling it and deploying it to a server
</aside>

<..>

## Functions

    # simple function, return keyword isn't required
    function hello {
        return 'Hello world!'
    }

    # with parameters... notice string interpolation (when using " versus ')
    function helloPerson($name) {
        "Hello $name!"
    }

    # with default parameters...
    function customizableHello($name, $greeting = 'Hello') {
        "$greeting $name!"
    }

<aside class="notes" data-markdown>
  demo this in ISE, copy/paste into editor
  * call the various functions
  * show tab completion for parameter names
  * `helloFullName -name Mo -greeting 'Greetings and Salutations'`
</aside>

<..>

## Functions continued

    # more common way to use parameters
    #   - mainly because it works for standalone script files
    #   - arguably cleaner formatting with attributes
    # [switch] type makes for convenient boolean existence parameters
    function helloOtherPerson {
        param (
            [Parameter(Mandatory)]
            [string]
            $name,

            [switch]
            $isMorning
        )

        if ($isMorning) {
            Write-Host -foregroundColor Yellow "Good morning, $name"
        }
        else {
            Write-Host -foregroundColor Cyan "Good day, $name"
        }
    }

<aside class="notes" data-markdown>
  * Don't remember your console colors?

    Write-Host -foregroundColor
    [System.ConsoleColor]::GetNames([System.ConsoleColor])

  * still just .NET!
</aside>

<..>

## Conditionals, Looping and the Rest

    # $false exists, too
    if ($true) { 'must be true then' }

    # -eq, -ne, -ge, -gt, -lt, -le, -match, -notmatch and more
    if ($val -eq 42) {
        'Life, the Universe and Everything'
    }

    # standard for loop is here, too
    foreach ($val in 1,2,3,4,5) {
        $val
    }

    # of course, while and do/while exist, too
    do {
        Write-Host "Waiting for condition"
    } until ($condition)

<aside class="notes" data-markdown>
    * pretty standard stuff here
    * don't get too freaked out by `-eq`... remember the redirection operator?
      * somecmd.exe > file.txt
      * that wouldn't work if we used `>` so we use `-ge` instead
    * side note, curly braces are *required*
</aside>

<..>

## Pipeline

    # optionally can use $PSItem instead of $_ (new in v3)
    1,2,3,4,5 | foreach { $_ }

    # MANY commands in PS work off of the pipeline
    Get-ChildItem -inc * -rec | Measure Length -Sum

    # GREAT for filtering things down
    get-childitem -path c:\path\to\logs -filter *.log -recurse |
        where-object { $_.LastWriteTime -lt [DateTime]::Today.Subtract([TimeSpan]::FromDays(365)) } |
        foreach-object { Move-Item $_ c:\some\archive }

    # quick aliases you might see
    where { } ... ?{ }
    foreach { } ... %{ }

<aside class="notes" data-markdown>
  * if it helps, think of it like LINQ
    * in fact, pipelines behave like LINQ in that it is very much like a yield statement
    * the thing producing the pipeline would be like the yield statement
    * the pipeline takes over and, when it is complete, it asks for the next item
    * if need be, show below example:

    function pipelineStart {
        foreach ($val in 1..10) {
            Write-Host "producing pipeline for $val"
            $val
        }
    }

    pipelineStart | foreach { Write-Host "in pipeline $_" }
</aside>
