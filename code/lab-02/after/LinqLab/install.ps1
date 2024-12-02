[CmdletBinding()]

param([Parameter(HelpMessage='Uninstall before installing')]
    [ValidateNotNullOrEmpty()]
    [switch]
    $reinstall)

if ($reinstall -eq $true)
{
    &.\uninstall.ps1
}

dotnet build

$pathToDebugFolder = Join-Path $PSScriptRoot 'src\LinqLab.ConsoleUi\bin\Debug'

Write-Host "Installing LinqLab.ConsoleUi from $pathToDebugFolder"

dotnet tool install --global --add-source "$pathToDebugFolder" LinqLabConsoleUi
