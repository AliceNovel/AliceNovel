namespace AliceNovel;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        // Check Default AppTheme
        if (Application.Current.RequestedTheme == AppTheme.Light)
            switchThemeToLight.IsChecked = true;
        else if (Application.Current.RequestedTheme == AppTheme.Dark)
            switchThemeToDark.IsChecked = true;
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
