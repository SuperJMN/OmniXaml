$ErrorActionPreference = "Stop"

. ".\include.ps1"

foreach($pkg in $Packages) 
{
    rm -Force -Recurse .\$pkg -ErrorAction SilentlyContinue
}

rm -Force -Recurse *.nupkg -ErrorAction SilentlyContinue
Copy-Item template OmniXAML -Recurse
sv lib "OmniXAML\lib\portable-windows8+net45"

mkdir $lib -ErrorAction SilentlyContinue

Copy-Item ..\Source\Glass\bin\Release\Glass.dll $lib
Copy-Item ..\Source\OmniXaml\bin\Release\OmniXaml.dll $lib

foreach($pkg in $Packages)
{
    (gc OmniXAML\$pkg.nuspec).replace('#VERSION#', $args[0]) | sc $pkg\$pkg.nuspec
}

foreach($pkg in $Packages)
{
    nuget.exe pack $pkg\$pkg.nuspec
}

foreach($pkg in $Packages)
{
    rm -Force -Recurse .\$pkg
}