# Real World Uses of PowerShell

<..>

# Build Scripts!

<..>

## The most common build script library is Psake

<blockquote><a href="https://github.com/psake/psake">psake</a> is pronounced
sake – as in Japanese rice wine. It does NOT rhyme with make, bake, or
rake.</blockquote>

It follows in the steps of tools like Rake to write build scripts in a DSL instead of in XML.

<..>

## A simple example

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
   
<aside class="notes" data-markdown>
* at it's most simple level, it is just a dependency tree of tasks
</aside>
    
<..>
    
## A more involved example

<pre><code>properties {
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
}</code></pre>

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
  * We of course call msbuild
  * We run unit tests
  * We added a call to `aspnet_compiler` to verify our Razor views 
* Things we *don't* do
  * We don't zip up our builds... we let TeamCity do this
  * We don't name our builds... again, TeamCity does this

<aside class="notes" data-markdown>
* look at multicorebuild property
* look at compileAspNetViews
</aside>
