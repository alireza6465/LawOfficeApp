# build.ps1 - Builds and publishes a self-contained single-file exe (win-x64) and optionally compiles an Inno Setup installer.
param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64"
)

$project = "src\LawOfficeApp.csproj"
$publishDir = Join-Path $PSScriptRoot "publish"

Write-Host "Cleaning previous publish..."
Remove-Item $publishDir -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "Running dotnet publish..."
dotnet publish $project -c $Configuration -r $Runtime /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=true -o $publishDir

if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed."
    exit $LASTEXITCODE
}

Write-Host "Published to $publishDir"

# Build installer using Inno Setup if ISCC is available
$iss = Join-Path $PSScriptRoot "installer_scripts\lawoffice_installer.iss"
$iscc = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
if (Test-Path $iscc) {
    Write-Host "Found Inno Setup compiler, building installer..."
    & $iscc $iss
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Inno Setup compilation failed."
        exit $LASTEXITCODE
    }
    Write-Host "Installer created."
} else {
    Write-Host "Inno Setup not found at $iscc. Skipping installer creation. Install Inno Setup or use GitHub Actions workflow to build installer on CI."
}
