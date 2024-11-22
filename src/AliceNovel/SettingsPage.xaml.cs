using AliceNovel.Resources.Strings;
using System.Globalization;

namespace AliceNovel;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        // Check Default AppLanguage
        CheckAppLanguage();

        // Check Default AppTheme
        if (Application.Current.RequestedTheme == AppTheme.Light)
            switchThemeToLight.IsChecked = true;
        else if (Application.Current.RequestedTheme == AppTheme.Dark)
            switchThemeToDark.IsChecked = true;
    }

    private async void SwitchLanguage(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        var selectedRadioButton = (RadioButton)sender;
        string selectedLanguage = "en-US";

        if (selectedRadioButton == languageSetting_enUS)
            selectedLanguage = "en-US";
        else if (selectedRadioButton == languageSetting_jaJP)
            selectedLanguage = "ja-JP";

        if (CultureInfo.CurrentUICulture.ToString() == selectedLanguage)
            return;

        bool answer = await DisplayAlert(AppResources.Alert__Confirm_, AppResources.Alert__RebootDescriptions_, AppResources.Alert__Reboot_, AppResources.Alert__Canncel_);
        if (answer != true)
        {
            CheckAppLanguage();
            return;
        }

        // Save the state in local
        Preferences.Default.Set("AppLanguage", selectedLanguage);

        Application.Current.Windows[0].Page.Dispatcher.Dispatch(() =>
        {
            CultureInfo culture = new(selectedLanguage, false);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.Current.Windows[0].Page = new AppShell();
        });
    }

    /// <summary>
    /// Set default Language for the Application
    /// </summary>
    void CheckAppLanguage()
    {
        if (CultureInfo.CurrentUICulture.ToString() == "ja-JP")
            languageSetting_jaJP.IsChecked = true;
        else
            languageSetting_enUS.IsChecked = true;
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
