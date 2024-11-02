# Development Guide

This page contains steps to build and run the Alice Novel repository from source.

## Initial setup

### Windows

- Install VS 17.10 or newer
- Follow [these steps](https://learn.microsoft.com/dotnet/maui/get-started/installation?tabs=vswin) to include MAUI
- If building iOS with pair to Mac: Install current stable Xcode on your Mac. Install from the [App Store](https://apps.apple.com/us/app/xcode/id497799835?mt=12) or [Apple Developer portal](https://developer.apple.com/download/more/?name=Xcode)
- If you're missing any of the Android SDKs, Visual Studio should prompt you to install them. If it doesn't prompt you then use the [Android SDK Manager](https://learn.microsoft.com/xamarin/android/get-started/installation/android-sdk) to install the necessary SDKs.
- Install [Open JDK 17](https://learn.microsoft.com/en-us/java/openjdk/download#openjdk-17)

> [!note]
> If there is `NETSDK1005` error, try the method below.
> 
> ```shell
> dotnet restore src/AliceNovel.sln
> ```
> <!--
> `AliceNovel.csproj`
> ```diff xml
> + <TargetFrameworks>net8.0-windows10.0.19041.0</TargetFrameworks>
> - <TargetFrameworks>net8.0-android</TargetFrameworks>
> - <TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net8.0-windows10.0.19041.0</TargetFrameworks>
> - <TargetFrameworks Condition="!$([MSBuild]::IsOSPlatform('linux'))">$(TargetFrameworks);net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
> ```
> -->

### Mac

- Install [VSCode](https://code.visualstudio.com/download)
- Follow the steps for installing the .NET MAUI Dev Kit for VS Code: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-maui

### Linux

Details: [build/linux.md](./build/linux.md)

- Install [VSCode](https://code.visualstudio.com/download)
- Follow the steps for installing the .NET MAUI Dev Kit for VS Code: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-maui

## What branch should I use?

As a general rule:

- [master](https://github.com/AliceNovel/AliceNovel/tree/master)

## Sample games

### Samples

Please head over to the links in the [AliceNovel/Samples](https://github.com/AliceNovel/Samples) to get started.

## Stats

[![stats](https://repobeats.axiom.co/api/embed/ea9ac7f76b2f9836ec873b8fe0d65979bee3f78f.svg)]()
