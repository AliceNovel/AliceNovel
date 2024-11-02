# Translation guide

This page contains steps to translate this app.

## Initial setup

- Install Visual Studio latest (Windows only)

#### Translate existing languages

1. Open `AliceNovel/Resources/Strings/AppResources.resx`
1. You can translate the app!

#### Add new languages

1. Create `AliceNovel/Resources/Strings/AppResources.**.resx`  
(`**`: language-code or culture-code)
1. Add language-code or culture-code for every Platform

(You can find a list of culture codes [here](https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c).)

`AliceNovel/Platforms/iOS/Info.plist` and `AliceNovel/Platforms/MacCatalyst/Info.plist`
```diff xml
<!-- Example -->
<key>CFBundleLocalizations</key>
<array>
+   <string>de</string>
    <string>es</string>
    <string>fr</string>
    <string>ja</string>
+   <string>pt</string> <!-- Brazil -->
    <string>pt-PT</string> <!-- Portugal -->
+   <string>ru</string>
    <string>zh-Hans</string>
    <string>zh-Hant</string>
</array>
<key>CFBundleDevelopmentRegion</key>
<string>en</string>
```

`AliceNovel/Platforms/Windows/Package.appxmanifest`
```diff xml
<!-- Example -->
<Resources>
    <Resource Language="en-US"/>
+   <Resource Language="de-DE"/>
    <Resource Language="es-ES"/>
    <Resource Language="fr-FR"/>
    <Resource Language="ja-JP"/>
+   <Resource Language="pt-BR"/>
    <Resource Language="pt-PT"/>
+   <Resource Language="ru-RU"/>
    <Resource Language="zh-CN"/>
    <Resource Language="zh-TW"/>
</Resources>
```

(Android has no settings for translation)
