using System.Globalization;

namespace AliceNovel;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        // Check Default AppLanguage
        if (CultureInfo.CurrentUICulture.ToString() == "ja-JP")
            languageSetting_jaJP.IsChecked = true;
        else
            languageSetting_enUS.IsChecked = true;

        // Check Default AppTheme
        if (Application.Current.RequestedTheme == AppTheme.Light)
            switchThemeToLight.IsChecked = true;
        else if (Application.Current.RequestedTheme == AppTheme.Dark)
            switchThemeToDark.IsChecked = true;
    }

    private void SwitchLanguage(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        var selectedRadioButton = (RadioButton)sender;
        string selectedLanguage = "en-US";

        if (selectedRadioButton == languageSetting_enUS)
            selectedLanguage = "en-US";
        else if (selectedRadioButton == languageSetting_jaJP)
            selectedLanguage = "ja-JP";

        CultureInfo culture = new(selectedLanguage, false);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        //// Save the state in local
        Preferences.Default.Set("AppLanguage", selectedLanguage);
    }

    /// <summary>
    /// Restart the app
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LanguageSetting_Reboot(object sender, EventArgs e)
    {
        (Application.Current as App).MainPage.Dispatcher.Dispatch(() =>
        {
            (Application.Current as App).MainPage = new AppShell();
        });
    }

    private void SwitchTheme(object sender, CheckedChangedEventArgs e)
    {
        if (switchThemeToLight.IsChecked)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            // Save the state in local
            Preferences.Default.Set("AppTheme", "Light");
        }
        else if (switchThemeToDark.IsChecked)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            // Save the state in local
            Preferences.Default.Set("AppTheme", "Dark");
        }
    }
}
