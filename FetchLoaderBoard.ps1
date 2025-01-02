param (
    [Parameter(Mandatory=$false)]
    [string]$SessionStr = "53616c7465645f5f60772d2126ccfed9de82f2c505676d0305f930ca1fcf5063e64727bcfcb8d20b906060178217fdc69ddbe4a57ae47e22bb59e7fdeaea582f"
)

$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession

                    
$cookie = New-Object System.Net.Cookie 

$cookie.Name = "session"
$cookie.Value = $SessionStr
$cookie.Domain = "adventofcode.com"

$session.Cookies.Add($cookie);

$uri = "https://adventofcode.com/2024/leaderboard/private/view/3229725.json"
$Response = Invoke-WebRequest -Uri $uri -WebSession $session

$Stream = [System.IO.StreamWriter]::new("Leaderboard.json", $false)

try {
   $Stream.Write($Response.Content)
} finally {
   $Stream.Dispose()
}