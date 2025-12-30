# Real World Uses of PowerShell

<..>

## Quick Aside About Continuous Delivery

We try to follow continuous delivery at work... meaning that [any commit to our
mainline branch can go to
production](http://continuousdelivery.com/2010/08/continuous-delivery-vs-continuous-deployment/).
Related to this, we try to avoid the concept of [snowflake
servers](http://martinfowler.com/bliki/SnowflakeServer.html), a term coined by
Martin Fowler.

Practically speaking, the rest of my slides cover how we implement continuous
delivery with PowerShell.

<aside class="notes" data-markdown>
* every snowflake is unique from all other snowflakes
    * that makes snowflakes special
    * it makes servers... *bad*
</aside>

<..>

# Build Scripts!

<..>

## The most common build script library is Psake

> [psake](https://github.com/psake/psake) is pronounced
> sake – as in Japanese rice wine. It does NOT rhyme with make, bake, or
> rake.

It follows in the steps of tools like Rake to write build scripts in a DSL instead of in XML.

<..>

## A simple example

```powershell
Task default -Depends Test
Task Test -Depends Compile, Clean {
  "This is a test"
}

Task Compile -Depends Clean {
  "Compile"
}

Task Clean {
  "Clean"
}
```

<aside class="notes" data-markdown>
* at it's most simple level, it is just a dependency tree of tasks
</aside>

<..>

## A more involved example

```powershell
properties {
    $src = "../PowerShellHoster/"
    $solution = "$src/PowerShellHoster.sln"
    $buildPath = "$src/PowerShellHoster.App/bin/Debug/"
    $deployPath = "c:/temp"
}

Task default -depends Compile

Task Compile {
    msbuild $solution /nologo /v:quiet
}

Task Deploy -depends Compile {
    $buildDate = [DateTime]::Now.ToString("yyyy-MM-dd HH-mm-ss")
    Write-Zip $buildPath "$deployPath/$buildDate.zip"
}
```

<aside class="notes" data-markdown>
* the properties block is where you set up variables, constants and utility methods
* note that msbuild is already in your PATH
* `exec` makes sure the build fails if the command line call fails
  * by checking `$LASTEXITCODE`
* the `Write-Zip` cmdlet comes from PSCX
</aside>

<..>

## How we use it

* We use the TeamCity psake module from [psake-contrib](https://github.com/psake/psake-contrib)
  * It adds better status messages for TeamCity
* We use psake for our local builds in addition to prod builds
  * Utility methods are shared in `properties`
  * Allows for TeamCity-specific functionality plus debug builds locally
* We do more than just compile our code
  * We also run unit tests
  * We added a call to `aspnet_compiler` to verify our Razor views
* Things we _don't_ do
  * We don't zip up our builds... we let TeamCity do this
  * We don't name our builds... again, TeamCity does this

<aside class="notes" data-markdown>
* look at multicorebuild property
* look at compileAspNetViews
</aside>
