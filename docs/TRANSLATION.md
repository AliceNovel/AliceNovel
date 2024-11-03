# Translation guide

This page contains steps to translate this app.

## Initial setup

- Install Visual Studio latest (Windows only)

#### Translate existing languages

- English (en-US) - Default
- 日本語 (ja-JP)

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
    <string>pt</string> <!-- Brazil -->
    <string>pt-PT</string> <!-- Portugal -->
    <string>ru</string>
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
    <Resource Language="pt-BR"/>
    <Resource Language="pt-PT"/>
    <Resource Language="ru-RU"/>
    <Resource Language="zh-CN"/>
    <Resource Language="zh-TW"/>
</Resources>
```

(Android has no settings for translation)

3. Add culture-code into sources.

`AliceNovel/SettingsPage.xaml`
```diff xml
    <!-- Language -->
    <Label Text="{x:Static strings:AppResources.LanguageSetting}" />
    <RadioButton x:Name="languageSetting_enUS"
                 Content="English (en-US)"
                 GroupName="languageSetting"
                 CheckedChanged="SwitchLanguage" />
    <RadioButton x:Name="languageSetting_jaJP"
                 Content="日本語 (ja-JP)"
                 GroupName="languageSetting"
                 CheckedChanged="SwitchLanguage" />
+   <RadioButton x:Name="languageSetting_deDE"
+                Content="Deutsch (de-DE)"
+                GroupName="languageSetting"
+                CheckedChanged="SwitchLanguage" />
```

`AliceNovel/SettingsPage.xaml.cs`
```diff cs
    var selectedRadioButton = (RadioButton)sender;
    string selectedLanguage = "en-US";

    if (selectedRadioButton == languageSetting_enUS)
        selectedLanguage = "en-US";
    else if (selectedRadioButton == languageSetting_jaJP)
        selectedLanguage = "ja-JP";
+   else if (selectedRadioButton == languageSetting_deDE)
+       selectedLanguage = "de-DE";

...

    void CheckAppLanguage()
    {
        if (CultureInfo.CurrentUICulture.ToString() == "ja-JP")
            languageSetting_jaJP.IsChecked = true;
+       else if (CultureInfo.CurrentUICulture.ToString() == "de-DE")
+           languageSetting_deDE.IsChecked = true;
        else
            languageSetting_enUS.IsChecked = true;
    }
```
