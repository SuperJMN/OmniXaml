$ErrorActionPreference = "Stop"

. ".\include.ps1"

foreach($pkg in $Packages) 
{
    rm -Force -Recurse .\$pkg -ErrorAction SilentlyContinue
}

rm -Force -Recurse *.nupkg -ErrorAction SilentlyContinue
Copy-Item template OmniXAML -Recurse
sv glass_lib "Glass\lib\portable-windows8+net45"
sv omnixaml_lib "OmniXAML\lib\portable-windows8+net45"
sv omnixaml_services_lib "OmniXaml.Services\lib\portable-windows8+net45"
sv omnixaml_services_dnfx_lib "OmniXaml.Services.DotNetFx\lib\portable-windows8+net45"
sv omnixaml_services_mvvm_lib "OmniXaml.Services.Mvvm\lib\portable-windows8+net45"
sv omnixaml_wpf_lib "OmniXaml.Wpf\lib\portable-windows8+net45"

mkdir $glass_lib -ErrorAction SilentlyContinue
mkdir $omnixaml_lib -ErrorAction SilentlyContinue
mkdir $omnixaml_services_lib -ErrorAction SilentlyContinue
mkdir $omnixaml_services_dnfx_lib -ErrorAction SilentlyContinue
mkdir $omnixaml_services_mvvm_lib -ErrorAction SilentlyContinue
mkdir $omnixaml_wpf_lib -ErrorAction SilentlyContinue

Copy-Item ..\Source\Glass\bin\Release\Glass.dll $glass_lib
Copy-Item ..\Source\OmniXaml\bin\Release\OmniXaml.dll $omnixaml_lib
Copy-Item ..\Source\OmniXaml.Services\bin\Release\OmniXaml.Services.dll $omnixaml_services_lib
Copy-Item ..\Source\OmniXaml.Services.DotNetFx\bin\Release\OmniXaml.Services.DotNetFx.dll $omnixaml_services_dnfx_lib
Copy-Item ..\Source\OmniXaml.Services.Mvvm\bin\Release\OmniXaml.Services.Mvvm.dll $omnixaml_services_mvvm_lib
Copy-Item ..\Source\OmniXaml.Wpf\bin\Release\OmniXaml.Wpf.dll $omnixaml_wpf_lib

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