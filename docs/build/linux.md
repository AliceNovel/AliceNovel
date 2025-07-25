# Linux

## Requirements

- Git
- .NET CLI (.NET 9 or later)
- Editor: 
  - [VSCode](https://code.visualstudio.com) (Recommend)
  - (Other Editor)

## Editor Setting - VSCode

See also: [.NET MAUI on Linux with VSCode](https://techcommunity.microsoft.com/t5/educator-developer-blog/net-maui-on-linux-with-visual-studio-code/ba-p/3982195) (Microsoft official documentation for .NET MAUI)

1. Install .NET 9 on Linux
    ```sh
    # Debian
    wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    sudo apt-get update && \
      sudo apt-get install -y dotnet-sdk-9.0
    # sudo apt-get update && \
    #   sudo apt-get install -y aspnetcore-runtime-9.0
    ```
1. Install .NET MAUI workloads
    ```sh
    dotnet workload install maui-android wasm-tools
    dotnet workload restore
    ```
1. Install Microsoft OpenJDK 17
    ```sh
    ubuntu_release=`lsb_release -rs`

    sudo apt-get install apt-transport-https
    sudo apt-get update
    sudo apt-get install msopenjdk-17

    sudo update-java-alternatives --set msopenjdk-17-amd64
    ```
1. Install VSCode
1. Install the .NET MAUI extension for VS Code
    - .NET MAUI Extension
    - C# Dev Kit Extension
    - C# Extension
      - Activate C# Dev Kit
1. Install Android Studio
1. Open the Alice Novel
1. Fix the issues
    - The solution could not be restored
        ```sh
        cd src
        dotnet restore
        ```
    - Android SDK not found
1. Connect your physical Android device or your emulator device
    - [Setup physical Android device for debug](https://learn.microsoft.com/dotnet/maui/android/device/setup?view=net-maui-9.0#connecting-over-wifi)
1. Edit and build the app
    ```sh
    # e.g.
    dotnet build -t:Run -f:net9.0-android -c:Debug \
      -p:AndroidSdkDirectory="/home/lemon73/Android/Sdk" \
      -p:JavaSdkDirectory="/usr/lib/jvm/msopenjdk-17-amd64" \
      /home/lemon73/Repo/AliceNovel/src/AliceNovel/AliceNovel.csproj
    # you can change into -t:Build and -c:Release
    ```

<!--
> [!note]
> If it will crash, add below
> ```xml
> // AliceNovel.csproj
> <PropertyGroup>
>     <AndroidFastDeployment>false</AndroidFastDeployment>
>     <AndroidUseSharedRuntime>false</AndroidUseSharedRuntime>
> </PropertyGroup>
> ```
> Then, copy `Resources/Strings/AppResources.ja.resx` into `Resources/Strings/AppResources.ja-JP.resx`
> And, build as "Release" (not as "Debug")
-->

## Instructions

You can run these commands using Konsole, PowerShell, Git Shell, or any other terminal. These instructions will assume the use of Command Prompt.

```sh
git clone https://github.com/AliceNovel/AliceNovel.git
cd AliceNovel/src/AliceNovel
```

### Running tests

In order to run tests from command line you need `dotnet cli`, available after you install Alice Novel or after you build from source. If you installed it, run the following commands (assuming ./AliceNovel is the root of your AliceNovel repository): 

```sh
cd src/AliceNovel
# dotnet watch run
# e.g.
dotnet build -t:Run -f:net9.0-android -c:Debug \
  -p:AndroidSdkDirectory="/home/lemon73/Android/Sdk" \
  -p:JavaSdkDirectory="/usr/lib/jvm/msopenjdk-17-amd64" \
  /home/lemon73/Repo/AliceNovel/src/AliceNovel/AliceNovel.csproj
```

### Build `.apk` for Android

```sh
# On `AliceNovel` directory
dotnet publish -f:net9.0-android -c:Release -p:AndroidSdkDirectory=~/Android/Sdk
# (AndroidSdk ... From Android Studio)
```
