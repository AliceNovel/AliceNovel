namespace Alice_Novel;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        //ページの読み込み
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//GamePage");
    }
}
