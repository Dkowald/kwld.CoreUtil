param(
    [string]$PackageVersion
)

set-location $PSScriptRoot

if([string]::IsNullOrWhiteSpace($PackageVersion)){throw "Version not set"}

$file = "../Readme.md"
if(!(test-path -path $file)){throw "Cannot find target file: $file";}
$content = (Get-Content $file)

$src = "(docs/Home.md)"
$target = "(" + [io.path]::Combine("https://github.com/Dkowald/kwld.CoreUtil/blob/", $PackageVersion, "Readme.md") + ")"
$content = $content.replace($src, $target)

$src= "(https://github.com/Dkowald/kwld.CoreUtil)"
$target = "(" + [io.path]::Combine("https://github.com/Dkowald/kwld.CoreUtil/blob/", $PackageVersion) + ")"
$content = $content.replace($src, $target)

$src= "(https://www.nuget.org/packages/kwld.CoreUtil/)"
$target = "(" + [io.path]::Combine("https://www.nuget.org/packages/kwld.CoreUtil/", $PackageVersion) + ")"
$content = $content.replace($src, $target)

Set-Content -path $file -Value $content
