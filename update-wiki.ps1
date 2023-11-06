param(
    [string]$PackageVersion
)
$footer = "./wiki/_Footer.md"

if([string]::IsNullOrWhiteSpace($PackageVersion)){throw "Version not set"}

if(!(test-path -path $footer)){throw "Cannot find target file";}

(Get-Content $footer).replace('{{PackageVersion}}', $PackageVersion) | Set-Content $footer

