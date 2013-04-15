# Automating server configuration

<..>

## We've automated everything else, why not the server configuration?

We can go from a fresh Windows Server 2008 R2 (or 2012) VM to a fully configured web server in under 5 minutes.

<..>

## Useful Modules and Cmdlets to Have

* WebAdministration
  * IIS automation
* Get|Add-WindowsFeature
  * server-OS only, Windows 8 has Get|Enable-WindowsOptionalFeature cmdlets
* PKI
  * Certificate automation (i.e. SSL)

<..>

## How we configure servers

* We actually use [Chef](https://github.com/opscode/chef) to configure servers
  * It just happens to call *a lot* of PowerShell
* We install utilities...
  * Sysinternals, Chrome, Sublime Text 2, 3rd party tools, etc.
* We configure...
  * machine.config changes
  * MSDTC
  * permissions
  * IIS
  * hosts

<..>

## Quick IIS examples

    # needed for the IIS: psdrive
    Import-Module WebAdministration

    # list all started appPools
    ls ./AppPools | where State -eq 'Started'

    # see if an appPool is 32-bit or not
    Get-Item .\AppPools\MyAppPoolName | select enable32BitAppOnWin64

    # create a new website
    New-Website 'powershell-yay' -PhysicalPath 'c:\temp'

<aside class="notes" data-markdown>
* gvim c:\temp\index.html
* save and pull up localhost
</aside>
