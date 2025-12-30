properties {
    $src = "../PowerShellHoster/"
    $solution = "$src/PowerShellHoster.sln"
    $buildPath = "$src/PowerShellHoster.App/bin/Debug/"
    $deployPath = "c:/temp"
}

Task default -depends Compile

Task Compile {
    Exec {
        msbuild $solution /nologo /v:quiet
    }
}

Task Deploy -depends Compile {
    $buildDate = [DateTime]::Now.ToString("yyyy-MM-dd HH-mm-ss")
    Write-Zip $buildPath "$deployPath/$buildDate.zip"
}
