# Linux

## Requirements
- Git Bash
- .NET 8.0 or later (.NET CLI)
- Editor: 
  - [Visual Studio Code](https://code.visualstudio.com) (Recommend)
  - (Other Editor)

## Editor setting - VSCode
More details : [.NET MAUI on Linux with Visual Studio Code](https://techcommunity.microsoft.com/t5/educator-developer-blog/net-maui-on-linux-with-visual-studio-code/ba-p/3982195) (Microsoft official documentation for .NET MAUI)
1. Install .NET 8 on Linux
    ```bash
    # Debian
    wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    sudo apt-get update && \
      sudo apt-get install -y dotnet-sdk-8.0
    sudo apt-get update && \
      sudo apt-get install -y aspnetcore-runtime-8.0
    ```
1. Install .NET MAUI Android workload
    ```bash
    dotnet workload install maui-android
    ```
1. Install OpenJDK 11
    ```bash
    ubuntu_release=`lsb_release -rs`

    sudo apt-get install apt-transport-https
    sudo apt-get update
    sudo apt-get install msopenjdk-11
    ```
1. Install Android Studio
1. Install Visual Studio Code
1. Install the .NET MAUI extension for VS Code
    - .NET MAUI Extension
    - C# Dev Kit Extension
    - C# Extension
1. Activate C# Dev Kit
1. Open the Alice Novel
1. Fix the issues
    - The solution could not be restored
    - Android SDK not found
1. Edit and build the app

> [!important]
> You cann't use some function in VSCode debugger. ex) Alart of community tool kit

## Instructions
You can run these commands using Konsole, PowerShell, Git Shell, or any other terminal. These instructions will assume the use of Command Prompt.
```shell
git clone https://github.com/AliceNovel/AliceNovel.git
cd AliceNovel/Alice_Novel
```

### Running tests
In order to run tests from command line you need `dotnet cli`, available after you install Alice Novel or after you build from source. If you installed it, run the following commands (assuming .\AliceNovel is the root of your AliceNovel repository): 
```shell
cd Alice_Novel/Alice_Novel
dotnet watch run
```

### Build `.apk` for Android
```shell
# On `Alice_Novel` directory
dotnet publish -f:net8.0-android -c:Release -p:AndroidSdkDirectory=~/Android/Sdk
```
(AndroidSdk ... From Android Studio)

---
*Reference*
- https://techcommunity.microsoft.com/t5/educator-developer-blog/net-maui-on-linux-with-visual-studio-code/ba-p/3982195
