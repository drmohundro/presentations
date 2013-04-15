# PowerShell Modules

<..>

## What are they?

<blockquote>A module is a set of related Windows PowerShell functionalities
that can be dynamic or that can persist on disk. Modules that persist on disk
are referenced, loaded, and persisted as script modules, binary modules, or
manifest modules.</blockquote>

(via [MSDN](http://msdn.microsoft.com/en-us/library/windows/desktop/dd878324.aspx))

<..>

## So... seriously, what are they?

* Version 2 of Snap-ins
* Used to share functionality that is bigger than a single script
* Usually you won't *write* modules, you'll *use* them

<aside class="notes" data-markdown>
* if you've heard of PSSnapins, Modules are the "v2" of Snap-ins
* *mostly* used for sharing code that is bigger than a script
  * with just scripts, you basically would share a zip of multiple files and then add them to your PATH 
</aside>

<..>

## Using Modules

    # list available modules (there are quite a few)
    Get-Module -ListAvailable
    
    # import a module
    # note: as of v3, not usually needed*
    Import-Module WebAdministration
    
    # use new and awesome module functionality
    cd IIS:\Sites
   
<aside class="notes" data-markdown>
* if you've heard of PSSnapins, Modules are the "v2" of Snap-ins
* *mostly* used for sharing code that is bigger than a script
  * with just scripts, you basically would share a zip of multiple files and then add them to your PATH 
* v3 auto-imports module *commands* but doesn't automatically load other functionality (like PSDrives)
* One quick, important note... some modules require 1) administrative priviledges, 2) 64-bit processes, etc.
</aside>

<..>

## Writing Modules

* Simplest way is to put functions in a `PSM1` file
  * Then call `Export-ModuleMember` on public commands
* More advanced modules can include things like:
  * [Compiled assemblies](http://msdn.microsoft.com/en-us/library/windows/desktop/dd878342.aspx)
  * [Manifests](http://msdn.microsoft.com/en-us/library/windows/desktop/dd878337.aspx)
    * Not required, but can add additional details about the module
* Look at [posh-git](https://github.com/dahlbyk/posh-git/) for good, simple example

<..>

## Wouldn't it be nice if I could install OSS modules?

I've found that, when I have a good idea, someone has usually beat me to it.

Check out [http://psget.net/](http://psget.net/)

    # once installed, you can do things like this:
    Import-Module Find-String
    Import-Module posh-git
    # etc.
