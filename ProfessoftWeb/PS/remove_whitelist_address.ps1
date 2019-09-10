$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri http://ExchangeMB/PowerShell/ -Authentication Kerberos # -Credential $Credential
$output = Import-PSSession $Session -DisableNameChecking -CommandName *ContentFilterConfig
$list = (Get-ContentFilterConfig).BypassedSenders
$list.remove($args[0])
Set-ContentFilterConfig -BypassedSenders $list
echo "Adres: $args[0] dodany do białej listy"
Remove-PSSession $Session