# Changelog of Alice Novel

## v1.1.0 GA
**Release Date**: TBD

**Plan**
- Add the plugin system of C#. ([#16](https://github.com/AliceNovel/AliceNovel/issues/16))

## v1.0.0 GA
**Release Date**: TBD

**Plan**
- Add the story branch function. ([#6](https://github.com/AliceNovel/AliceNovel/issues/6))

## v0.9.3
**Release Date**: TBD

**Changed**
- GPL v3 to MIT license. ([#70](https://github.com/AliceNovel/AliceNovel/issues/70))

## [v0.9.2](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.2)
**Release Date**: 2024/5/6

**Changed**
- User interface. ([#48](https://github.com/AliceNovel/AliceNovel/issues/48))
  - Splash screen of Android.
  - Icon design. ([#50](https://github.com/AliceNovel/AliceNovel/issues/50))
  - Theme colors. ([#55](https://github.com/AliceNovel/AliceNovel/issues/55))
- Update dependencies. ([#51](https://github.com/AliceNovel/AliceNovel/issues/51))
  - Nuget
    - Microsoft.Maui.Controls
    - Microsoft.Maui.Controls.Compatibility
    - CommunityToolkit.Maui
    - CommunityToolkit.Maui.MediaElement
  - GitHub Actions
    - actions/upload-artifacts
- Improve regular expression ([#63](https://github.com/AliceNovel/AliceNovel/issues/63))

**Added**
- Some documentations. ([#66](https://github.com/AliceNovel/AliceNovel/issues/66))

**Fixed**
- Azure CI/CD. ([#57](https://github.com/AliceNovel/AliceNovel/issues/57))
- Dependabot. ([#59](https://github.com/AliceNovel/AliceNovel/issues/59))
- [Android] Can't move file loading. ([#49](https://github.com/AliceNovel/AliceNovel/issues/49))
- Old version file is not working. ([#62](https://github.com/AliceNovel/AliceNovel/issues/62))

## [v0.9.1](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.1)
**Release Date**: 2024/4/6

**Changed**
- CI/CD to change output file format. ([#44](https://github.com/AliceNovel/AliceNovel/issues/44))

**Added**
- Shortcut key for save and button. ([#19](https://github.com/AliceNovel/AliceNovel/issues/19))

## [v0.9.0 GA](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0)
**Release Date**: 2024/4/2

**Changed**
- Change fonts (Replace _Open Sans_ with _Noto Sans_). ([#35](https://github.com/AliceNovel/AliceNovel/issues/35))

**Added**
- CHANGELOG file. ([#24](https://github.com/AliceNovel/AliceNovel/issues/24))
- Documentation for developer. ([#32](https://github.com/AliceNovel/AliceNovel/issues/32))
- Documentation for contributer. ([#39](https://github.com/AliceNovel/AliceNovel/issues/39))

**Fixed**
- Main color theme for Android. ([#38](https://github.com/AliceNovel/AliceNovel/issues/38))

## [v0.9.0-rtm](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-rtm)
**Release Date**: 2024/1/8

**Changed**
- Upgrade of framework (.NET). Upgraded .NET7 to 8.
- Cache effecient. ([#9](https://github.com/AliceNovel/AliceNovel/issues/9))

**Fixed**
- Not to do proceed until the video is finished. ([#7](https://github.com/AliceNovel/AliceNovel/issues/7))
- Disabled UI. ([#13](https://github.com/AliceNovel/AliceNovel/issues/13))
- Play the other movie. ([#22](https://github.com/AliceNovel/AliceNovel/issues/22))

## [v0.9.0-rc3](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-rc3)
**Release Date**: 2023/10/27

**Added**
- The function to save and load of the game data.
- The function to read and play movie files. ([#4](https://github.com/AliceNovel/AliceNovel/issues/4))

**Fixed**
- Can't open another .anproj file. ([#1](https://github.com/AliceNovel/AliceNovel/issues/1))

## [v0.9.0-rc2](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-rc2)
**Release Date**: 2023/10/14

**Added**
- The function to read and play sound files.

## [v0.9.0-rc1](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-rc1)
**Release Date**: 2023/10/8

**Added**
- The function to read `.anproj` file.
- The function to show background images.

**Removed**
- The function to read `.anov` file.

## [v0.9.0-beta](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-beta)
**Release Date**: 2023/9/3

**Changed**
- When reading `.anov` file, previously per line. But this version support reading multi lines.

## [v0.9.0-alpha](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.9.0-alpha)
**Release Date**: 2023/8/25

**Added**
- Main UI.
- The function to read anov syntax (`.anov`) file per line.
  - `> `: read place information (preview / no function)
  - `- `: read people information (preview)
  - `/ `: read emotion information (preview / no function)
  - `[` and `]`: read speech information

## [v0.8.0-alpha](https://github.com/AliceNovel/AliceNovel/releases/tag/v0.8.0-alpha)
**Release Date**: 2023/5/3

**Added**
- Only User Interface.
