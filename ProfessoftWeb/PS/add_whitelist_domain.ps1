$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri http://ExchangeMB/PowerShell/ -Authentication Kerberos # -Credential $Credential
$output = Import-PSSession $Session -DisableNameChecking -CommandName *ContentFilterConfig
$list = (Get-ContentFilterConfig).BypassedSenderargs[0]s
$list.add($args[0])
Set-ContentFilterConfig -BypassedSenders $list
echo "Domena: $args[0] dodana do białej listy"
Remove-PSSession $Session