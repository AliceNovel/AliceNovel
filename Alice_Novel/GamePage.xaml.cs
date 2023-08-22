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

    //初期状態のボタン有効/無効の確認用(nullエラー対策のために初期値はfalseに設定)
    bool button3_start, button4_start, button5_start, button6_start = false;

    private void ReShow_Clicked(object sender, EventArgs e)
    {
        //reをクリックしたときの処理
        button1.IsVisible = true;
        button2.IsVisible = true;
        textbox.IsVisible = true;
        textbox_out.IsVisible = true;
        re.IsEnabled = false;
        //image.Aspect = "AspectFill";
        //初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
        button3.IsVisible = button3_start;
        button4.IsVisible = button4_start;
        button5.IsVisible = button5_start;
        button6.IsVisible = button6_start;
    }

    private void Button1_Clicked(object sender, EventArgs e)
    {
        //button1をクリックしたときの処理
    }

    private void Button2_Clicked(object sender, EventArgs e)
    {
        //button2をクリックしたときの処理

        //初期のボタン有効/無効状態を確認
        button3_start = button3.IsVisible;
        button4_start = button4.IsVisible;
        button5_start = button5.IsVisible;
        button6_start = button6.IsVisible;

        button1.IsVisible = false;
        button2.IsVisible = false;
        button3.IsVisible = false;
        button4.IsVisible = false;
        button5.IsVisible = false;
        button6.IsVisible = false;
        textbox.IsVisible = false;
        textbox_out.IsVisible = false;
        re.IsEnabled = true;
        //image.Aspect = "AspectFit";
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
        //ファイル読み込み処理
        button5.Text = "button5";
        textbox.Text = "Hello!";
        button6.IsVisible = true;
        game_ui.Title = "Game Title";
    }

    private void Button6_Clicked(object sender, EventArgs e)
    {
        //button6をクリックしたときの処理
    }
}
