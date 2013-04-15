## So... what is this
# PowerShell thing?

<..>

<blockquote>Windows PowerShell is Microsoft's task automation framework, consisting of a
command-line shell and associated scripting language built on top of .NET
Framework. PowerShell provides full access to COM and WMI, enabling
administrators to perform administrative tasks on both local and remote Windows
systems.</blockquote>

via [wikipedia](http://en.wikipedia.org/wiki/Windows_PowerShell)

<..>

## More to the point

* Object-oriented
* Command line shell
* .NET-based scripting language
* Hostable
* Released in November 2006

<..>

### Whoa... a command line?
## Do we even NEED another command line?
### Isn't cmd.exe enough?

<..>

![No. Just. No.](/images/absolutely-not.jpg)
### Don't even say such nonsense.

<..>

<blockquote>We are digging ourselves out of a 30 year hole.</blockquote>

via [Jeffrey Snover's Lang.Net PowerShell Talk](http://blogs.msdn.com/powershell/archive/2009/04/17/15-minutes-with-lang-net.aspx)

<aside class="notes" data-markdown>
  Jeffrey Snover is the creator of PowerShell. Have you noticed that cmd.exe
  still looks like DOS? That's what he's talking about. Yes, the UNIX command
  line looks and behaves similarly but that environment has continued and
  thrived.
</aside>

<..>

### This has everything to do with Microsoft realizing that
## the GUI isn't always the right tool for the job

<aside class="notes" data-markdown>
  As an example, we discovered a couple years ago that, while SQL Server ships
  with a GUI installer, Microsoft doesn't use the GUI to install - they
  automate the install. The GUI is great for learning, but when speed and
  efficiency matter, it doesn't work.
</aside>

<..>

## Can you show me?

<..>

<pre>
PS C:\> dir dev


    Directory: C:\dev


Mode                LastWriteTime     Length Name
----                -------------     ------ ----
d----         3/12/2013  12:22 PM            changecontrol
d----         3/26/2013  10:18 AM            chef-repo
d----         3/12/2013  12:20 PM            console-framework
d----         3/25/2013   3:26 PM            deployer
d----         3/18/2013   2:58 PM            docs
d----         3/12/2013  12:21 PM            f5-manager
d----         3/19/2013   2:23 PM            HPP
d----         3/12/2013  12:21 PM            iis-local
d----         3/12/2013  12:23 PM            infrastructure
d----         3/19/2013   9:41 AM            IT-Documents
d----         3/12/2013  12:21 PM            node-cct-2
d----         3/12/2013  12:21 PM            NodeCCT
d----         3/22/2013   4:16 PM            samples
d----         3/22/2013   4:21 PM            ServiceU
d----         3/12/2013  12:22 PM            sql
d----         3/22/2013   2:57 PM            sublime-projects
</pre>

<aside class="notes" data-markdown>
  I recognize this isn't exciting, but... it is a command shell after all. We'll get more exciting quickly.
</aside>

<..>

## Okay, okay... we can do better...

<..>

<pre><code><span># Changing directories
cd c:\temp</span>

<span class="fragment"># Getting directory contents
dir</span>

<span class="fragment"># Making directories
mkdir foo</span>

<span class="fragment"># Again, changing directories
cd foo</span>

<span class="fragment"># Removing directories
rmdir foo</span>
</code></pre>

<..>

## Surely... that isn't all...

<..>

<pre><code><span># Searching all C# files for "console.writeline"
Get-ChildItem -include *.cs -recurse | Select-String console.writeline</span>

<span class="fragment"># Get all processes whose memory usage is over 125mb
Get-Process | where WorkingSet -ge 125mb</span>

<span class="fragment"># Get list of screensavers from registry
Get-ChildItem HKCU:/Software/Microsoft/Windows/CurrentVersion/Screensavers</span>

<span class="fragment"># Get info about physical memory modules
Get-WmiObject CIM_PhysicalMemory |
  Select-Object @{Name='Capacity';Expression = {$_.Capacity / 1gb}}, DataWidth, DeviceLocator, PartNumber, SerialNumber, Status, TotalWidth, TypeDetail |
  Format-Table -autosize</span>
</code></pre>

<aside class="notes" data-markdown>
  * grab all cs files recursively and search for the string console.writeline
  * grab all processes whose working set memory usage is over 125 mb
    * note that you can use identifiers like mb (kb, gb, tb, pb work, too... sorry, no exabytes yet)
  * use the registry provider to see which screensavers are available... useful? nah, just fun.
    * powershell has "PSProviders" to emulate file systems for things that aren't inherently file systems
      * like aliases, variables, functions, registry, environment variables, IIS websites/apppools, and more
      * you can write your own
    * notice we used the same command that lists files
  * query WMI to see about physical memory and capacity on the box (can be remote, too)
</aside>

<..>

## How about building reports?

<pre><code>$prodServers = '...'
$prodServers |
  foreach { Get-Service -ComputerName $_ |
    where { $_.DisplayName -match '(SUE|ServiceU)' -or $_.Name -match '^Su\w+' }
  } |
  select @{
    Name='Role';
    Expression={
      switch -regex ($_.MachineName) {
        '^web' { 'Web' }
        default { 'Email' }
      }
    }
  }, Name, DisplayName |
  select -unique Role, Name, DisplayName |
  ConvertTo-Html |
  Out-File .\WindowsServicesReport.html -Encoding ASCII</code></pre>

<..>

## So... actually pretty powerful after all
