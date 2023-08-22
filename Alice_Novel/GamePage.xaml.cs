namespace Alice_Novel;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    //初期状態のボタン有効/無効の確認用(nullエラー対策のために初期値はfalseに設定)
    bool button1_start, button2_start, button3_start, button4_start, button5_start, button6_start = false;
    //UI表示/非表示
    bool ui_visible = true;

    private void ReShow_Clicked(object sender, EventArgs e)
    {
        //画面をクリックしたときの処理
        if (ui_visible == true)
        {
            //テキストを進める処理
            talkname.Text = ".NET";
            if (textbox.Text == "Next...")
                textbox.Text = "Text";
            else
                textbox.Text = "Next...";

        }
        else
        {
            //UI再表示処理
            talkname.IsVisible = true;
            textbox.IsVisible = true;
            textbox_out.IsVisible = true;
            //初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
            button1.IsVisible = button1_start;
            button2.IsVisible = button2_start;
            button3.IsVisible = button3_start;
            button4.IsVisible = button4_start;
            button5.IsVisible = button5_start;
            button6.IsVisible = button6_start;
            ui_visible = true;
        }
    }

    private void Button1_Clicked(object sender, EventArgs e)
    {
        //button1をクリックしたときの処理
    }

    private void Button2_Clicked(object sender, EventArgs e)
    {
        //button2をクリックしたときの処理

        //初期のボタン有効/無効状態を確認
        button1_start = button1.IsVisible;
        button2_start = button2.IsVisible;
        button3_start = button3.IsVisible;
        button4_start = button4.IsVisible;
        button5_start = button5.IsVisible;
        button6_start = button6.IsVisible;
        //画像以外すべて非表示
        button1.IsVisible = false;
        button2.IsVisible = false;
        button3.IsVisible = false;
        button4.IsVisible = false;
        button5.IsVisible = false;
        button6.IsVisible = false;
        talkname.IsVisible = false;
        textbox.IsVisible = false;
        textbox_out.IsVisible = false;
        ui_visible = false;
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
        talkname.Text = ".NET";
        textbox.Text = "ようこそ!";
        button5.IsVisible = false;
        //button6.IsVisible = true;
        game_ui.Title = "Game Title";
    }

    private void Button6_Clicked(object sender, EventArgs e)
    {
        //button6をクリックしたときの処理
    }
}
