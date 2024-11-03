namespace AliceNovel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set Application language (from Local Data)
            string userAppLanguage = Preferences.Default.Get("AppLanguage", "Default");
            if (userAppLanguage != "Default")
                System.Globalization.CultureInfo.CurrentUICulture = new(userAppLanguage);

            MainPage = new AppShell();

            // Set Application Theme (from Local Data)
            string userAppTheme = Preferences.Default.Get("AppTheme", "Default");
            if (userAppTheme == "Light")
                Current.UserAppTheme = AppTheme.Light;
            else if (userAppTheme == "Dark")
                Current.UserAppTheme = AppTheme.Dark;
        }

        protected override Window CreateWindow(IActivationState activationState) {
            var window = base.CreateWindow(activationState);
            // if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            //     window.Title = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            window.Title = "Alice Novel";
            return window;
        }
    }
}