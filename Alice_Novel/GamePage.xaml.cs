namespace Alice_Novel;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    //画面の比率によって画像の中心位置変更
    private void Game_ui_SizeChanged(object sender, EventArgs e)
    {
        /*
        double height = main_ui.Height;
        double width = main_ui.Width;

        if (height > width)
        {
            //縦UI
            image.VerticalOptions = LayoutOptions.Start;
        }
        else
        {
            //横UI
            image.VerticalOptions = LayoutOptions.Center;
        }
        */
    }

    private void Button1_Clicked(object sender, EventArgs e)
    {
        //button1をクリックしたときの処理
    }

    private async void Button2_Clicked(object sender, EventArgs e)
    {
        //button2をクリックしたときの処理
        await Shell.Current.GoToAsync("//MainPage");
    }

    private void Button3_Clicked(object sender, EventArgs e)
    {
        //button3をクリックしたときの処理
    }

    private void Button4_Clicked(object sender, EventArgs e)
    {
        //button4をクリックしたときの処理
    }

    private void Button5_Clicked(object sender, EventArgs e)
    {
        //button5をクリックしたときの処理
    }

    private void Button6_Clicked(object sender, EventArgs e)
    {
        //button6をクリックしたときの処理
    }
}