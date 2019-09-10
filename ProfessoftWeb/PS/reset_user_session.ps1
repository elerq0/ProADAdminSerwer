$tssrvArray = @("TS1Srv", "TS2Srv")
$login_status=0
foreach ($tssrv in $tssrvArray) {
    #echo "$tssrv"
	$session = ((quser /server:$tssrv.kglsa.local | Where-Object { $_ -match $args[0] }) -split ' +')
    $sessionId=$session[3]
    $userName=$session[1]
    if($sessionId)
    {
        #logoff $sessionId /server:$tssrv.kglsa.local  #LOGS USER OFF!!
        echo "User: $userName was logged off"
        $login_status=1
        break
    }
}
if(!$login_status)
{
     throw "Error: No such user currently logged in"
}
