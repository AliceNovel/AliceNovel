namespace Alice_Novel;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        //ページの読み込み
        InitializeComponent();
    }

    //画面の比率によって画像の中心位置変更
    private void Main_ui_SizeChanged(object sender, EventArgs e)
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

    private void button1_Clicked(object sender, EventArgs e)
    {
        //button1またはbutton1xをクリックしたときの処理
    }

    private void button2_Clicked(object sender, EventArgs e)
    {
        //button2またはbutton2xをクリックしたときの処理
    }

    private void button3_Clicked(object sender, EventArgs e)
    {
        //button3またはbutton3xをクリックしたときの処理
    }

    private void button4_Clicked(object sender, EventArgs e)
    {
        //button4またはbutton4xをクリックしたときの処理
    }

    private void button5_Clicked(object sender, EventArgs e)
    {
        //button5またはbutton5xをクリックしたときの処理
    }

    private void button6_Clicked(object sender, EventArgs e)
    {
        //button6またはbutton6xをクリックしたときの処理
    }
}
