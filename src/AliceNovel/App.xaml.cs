namespace Alice_Novel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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