<#
RS_ProgMode.ps1
(via http://www.withinwindows.com/2009/01/12/crash-course-on-authoring-windows-7-troubleshooting-packs/)

Resolve mode misconfiguration by setting it to its proper value (2)
#>

Write-DiagProgress -activity 'Enabling Programming mode...'

$mode = Set-ItemProperty `
    -path 'Registry::HKEY_CURRENT_USER\Software\Microsoft\Calc' `
    -name 'layout' `
    -type DWORD `
    -value '2'