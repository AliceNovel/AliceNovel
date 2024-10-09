# Windows

## Requirements
- Git Bash
- .NET 8.0 or later (.NET CLI)
- Editor: 
  - [Visual Studio 2022](https://visualstudio.microsoft.com) (Community Edition or better) (Recommend)
  - [Visual Studio Code](https://code.visualstudio.com)
  - (Other Editor)

## Instructions
You can move this app to use Visual Studio 2022.

## Using Visual Studio 2022
1. Install Visual Studio 2022
1. Install MAUI package in it
1. Open this repository in it

> [!note]
> If there is `NETSDK1005` error, try the method below.
> 
> `AliceNovel.csproj`
> ```diff xml
> + <TargetFrameworks>net8.0-windows10.0.19041.0</TargetFrameworks>
> - <TargetFrameworks>net8.0-android</TargetFrameworks>
> - <TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net8.0-windows10.0.19041.0</TargetFrameworks>
> - <TargetFrameworks Condition="!$([MSBuild]::IsOSPlatform('linux'))">$(TargetFrameworks);net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
> ```
