param (
    [Parameter(Mandatory=$false)]
    [string]$SessionStr = "53616c7465645f5f785f50e3cc8919248f6d1cc7af30f5cc73267f3412026024f8c229000498fba41ba7141047b68a8efd626b01016aca0fa328b304f1cf2a45"
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