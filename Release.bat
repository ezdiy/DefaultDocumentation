@ECHO off

DEL /q package
dotnet clean source\DefaultDocumentation\DefaultDocumentation.Release.csproj -c Release

dotnet build source\DefaultDocumentation\DefaultDocumentation.Release.csproj -c Release
dotnet pack source\DefaultDocumentation\DefaultDocumentation.Release.csproj -c Release -o package