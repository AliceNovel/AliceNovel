namespace AliceNovel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // For debug of multi-languages support
            // System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            MainPage = new AppShell();

            // Set Application Theme (from Local Data)
            string UserAppTheme = Preferences.Default.Get("AppTheme", "Default");
            if (UserAppTheme == "Light")
                Application.Current.UserAppTheme = AppTheme.Light;
            else if (UserAppTheme == "Dark")
                Application.Current.UserAppTheme = AppTheme.Dark;
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