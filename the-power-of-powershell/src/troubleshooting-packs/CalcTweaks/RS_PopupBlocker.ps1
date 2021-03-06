#RS_Popupblocker.ps1
#This is an example Resolver that enables the IE Popup blocker
#Note that this example does not provide any error handling


Write-DiagProgress -activity "Enabling Popup blocker..."

 $PopupMgr = get-itemproperty "Registry::HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\New Windows" "PopupMgr"

if($PopupMgr.PopupMgr -ieq "no")
{
    Set-ItemProperty -path "Registry::HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\New Windows" -name PopupMgr -value "yes"
}

if($PopupMgr.PopupMgr -eq "0")
{
    Set-ItemProperty -path "Registry::HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\New Windows" -name PopupMgr -value "1"
}
