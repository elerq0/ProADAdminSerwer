$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri http://ExchangeMB/PowerShell/ -Authentication Kerberos # -Credential $Credential
$output = Import-PSSession $Session -DisableNameChecking -CommandName *ContentFilterConfig
(Get-ContentFilterConfig).BypassedSenders
Remove-PSSession $Session