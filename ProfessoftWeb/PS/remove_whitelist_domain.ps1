$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri http://ExchangeMB/PowerShell/ -Authentication Kerberos # -Credential $Credential
$output = Import-PSSession $Session -DisableNameChecking -CommandName *ContentFilterConfig
$list = (Get-ContentFilterConfig).BypassedSenderargs[0]s
$list.remove($args[0])
Set-ContentFilterConfig -BypassedSenders $list
echo "Domena: $args[0] usunięta z białej listy"
Remove-PSSession $Session