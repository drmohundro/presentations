<#
TS_ProgMode.ps1
(via http://www.withinwindows.com/2009/01/12/crash-course-on-authoring-windows-7-troubleshooting-packs/)

Verify whether or not programming mode is enabled.
#>

Write-DiagProgress -activity 'Checking mode...'

$calc = Get-ItemProperty 'Registry::HKEY_CURRENT_USER\Software\Microsoft\Calc' 'layout'

Update-DiagRootCause -id RC_ProgModeIsNotEnabled -Detected ($calc.layout -ne 2)