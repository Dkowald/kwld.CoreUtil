param(
    [string]$PackageVersion
)

set-location $PSScriptRoot

if([string]::IsNullOrWhiteSpace($PackageVersion)){throw "Version not set"}

$file = "../Readme.md"
if(!(test-path -path $file)){throw "Cannot find target file: $file";}
$content = (Get-Content $file)

$src = "(docs/Home.md)"
$target = "(" + [io.path]::Combine("https://github.com/Dkowald/kwd.CoreUtil/blob/", $PackageVersion, "Readme.md") + ")"
$content = $content.replace($src, $target)

$src= "(https://github.com/Dkowald/kwd.CoreUtil)"
$target = "(" + [io.path]::Combine("https://github.com/Dkowald/kwd.CoreUtil/blob/", $PackageVersion) + ")"
$content = $content.replace($src, $target)

$src= "(https://www.nuget.org/packages/kwd.CoreUtil/)"
$target = "(" + [io.path]::Combine("https://www.nuget.org/packages/kwd.CoreUtil/", $PackageVersion) + ")"
$content = $content.replace($src, $target)

Set-Content -path $file -Value $content
