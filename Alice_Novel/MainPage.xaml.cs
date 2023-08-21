namespace Alice_Novel;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();//ページの読み込み
    }

    private async void Game_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//GamePage");//画面遷移
    }

    private async void Develop_Clicked(object sender, EventArgs e)
    {
    }

    private async void Setting_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SettingPage");//画面遷移
    }
}
