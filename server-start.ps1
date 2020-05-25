Push-Location "backend/Ibsys2"
Write-Output "dotnet run..."
$env:ASPNETCORE_URLS="http://*:5000";
Start-Job {
	Start-Sleep -s 10
	Start-Process "http://localhost:5000/simulation"
}
dotnet run