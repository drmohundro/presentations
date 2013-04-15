$promptCalls = New-Object System.Collections.ArrayList

function Shortened-Path {
    $pwd.Path.Replace($HOME, '~')
}

function prompt {
    Write-Host "$(Shortened-Path) " -nonewline

    $promptCalls | foreach { $_.Invoke() }

    Write-Host ''

    Write-Host '>' -nonewline

    ' '
}

function Add-CallToPrompt([scriptblock] $call) {
    [void]$promptCalls.Add($call)
}

Add-CallToPrompt { 
    $weather = Get-Weather 38002
    Write-Host "[$($weather.Temperature) $($weather.Condition)]" -foregroundColor Green -nonewline
}
