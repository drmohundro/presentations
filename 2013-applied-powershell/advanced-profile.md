## Let's Move On To...

# Profile Customizations

<..>

## What is a Profile?

* Defined by the `$profile` variable
  * Typically under your My Documents\WindowsPowerShell folder
* The PowerShell script that runs on session start
* $`profile` is to PowerShell as `.bashrc` is to Bash
* $`profile` is to PowerShell as `.zshrc` is to ZSH

<..>

## So, why is that a big deal?

* Custom aliases
* Custom functions
* PATH modifications
* Prompt modifications
* Import Modules
* Update type definitions
* And more

<..>

## Easiest way to get started is...

```powershell
# I'd recommend a different editor, but it probably won't be in your PATH at first...
notepad $profile
```

<..>

## Simple prompt examples

```powershell
# simple prompt, looks like $ _
function prompt {
  '$ '
}

# display the time in your prompt, looks like 9:51 AM> _
function prompt {
  "$([DateTime]::Now.ToShortTimeString())> "
}

# display the current directory (notice we have to put the '>' there)
# this one looks just like cmd.exe
function prompt {
  "$($pwd.Path)> "
}
```

<..>

## What about displaying the weather?

```powershell
function prompt {
    # note the -nonewline parameter
    Write-Host "$($pwd.Path) " -nonewline
    $weather = Get-Weather 38002

    # let's change the color for this (note you could make the color different depending on the conditions)
    # we do want a new line here
    Write-Host "[$($weather.Temperature) $($weather.Condition)]" -foregroundColor Green

    # starting second line... let's add some sort of thing here, like a '>'
    Write-Host ">" -nonewline

    # because Write-Host doesn't return anything, we still need to return something here
    ' '
}
```

<..>

## Prompt Tricks

* Instead of using `$pwd.Path`, I like to replace `$HOME` with '~'
* I have a `$promptCalls` variable that I use to add additional data to my prompt
  * Things like the weather
  * Alert data ("IIS isn't running", "Service isn't running", "Password expires tomorrow", etc.)
  * Source control status (git, hg, etc.)
    * Libraries like [posh-git](https://github.com/dahlbyk/posh-git) and [posh-hg](https://github.com/JeremySkinner/posh-hg) do this for you

<..>

## Profile Tips

* Put your profile in source control
  * Feel free to [fork mine on GitHub](https://github.com/drmohundro/powershellenv)
  * I put additional things up there like my VS settings and scripts
  * Might use an `EnvSpecificProfile.ps1` script
* Add things to your PATH
  * VS tools to PATH (msbuild, csc, etc.)
  * Sysinternals (I do this one globally)
* Customize type data
* "Dot-source" more scripts in
  * Allows for better organization

<aside class="notes" data-markdown>
Won't get into specifics about type-data except that:
* ps1xml files
* allows for additional metadata (dir listing isn't just byte size, but in KB, MB, etc.)
</aside>
