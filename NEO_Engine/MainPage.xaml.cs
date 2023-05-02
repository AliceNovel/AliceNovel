namespace NEO_Engine;

public partial class MainPage : ContentPage
{
	//int count = 0;

	public MainPage()
	{
		InitializeComponent();
    }

	/*
	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
	*/

    private void Main_ui_SizeChanged(object sender, EventArgs e)
    {
        double height = main_ui.Height;
        double width = main_ui.Width;

		if (height > width)
		{
            //縦UI
            //row_desigin.Height = 0;
            //column_desigin.Width = 500;
            image_desigin.HorizontalOptions = LayoutOptions.Center;
            image_desigin.VerticalOptions = LayoutOptions.Start;

            vertical_desigin.IsVisible = false;
            horizontal_desigin.IsVisible = true;

            /*
            button1.IsVisible = false;
            button2.IsVisible = false;
            button3.IsVisible = false;
            button4.IsVisible = false;
            button5.IsVisible = false;
            button6.IsVisible = false;

            button1x.IsVisible = true;
            button2x.IsVisible = true;
            button3x.IsVisible = true;
            button4x.IsVisible = true;
            button5x.IsVisible = true;
            button6x.IsVisible = true;
            */

            //画像サイズ
            image.HeightRequest = width - 40;
            image.WidthRequest = width - 40;

            /*
			vertical_desigin.HeightRequest = 0;
			vertical_desigin.WidthRequest = 0;
			button1.WidthRequest = 0;
			button2.WidthRequest = 0;
			button3.WidthRequest = 0;
			button4.WidthRequest = 0;
			button5.WidthRequest = 0;
			button6.WidthRequest = 0;

            horizontal_desigin.HeightRequest = 400;
            horizontal_desigin.WidthRequest = 400;
            button1x.WidthRequest = 250;
            button2x.WidthRequest = 250;
            button3x.WidthRequest = 250;
            button4x.WidthRequest = 250;
            button5x.WidthRequest = 250;
            button6x.WidthRequest = 250;
            */
        }
		else
		{
            //横UI
            //column_desigin.Width = 0;
            //row_desigin.Height = 500;
            image_desigin.HorizontalOptions = LayoutOptions.Start;
            image_desigin.VerticalOptions = LayoutOptions.Center;

            vertical_desigin.IsVisible = true;
            horizontal_desigin.IsVisible = false;

            /*
            button1.IsVisible = true;
            button2.IsVisible = true;
            button3.IsVisible = true;
            button4.IsVisible = true;
            button5.IsVisible = true;
            button6.IsVisible = true;

            button1x.IsVisible = false;
            button2x.IsVisible = false;
            button3x.IsVisible = false;
            button4x.IsVisible = false;
            button5x.IsVisible = false;
            button6x.IsVisible = false;
            */

            //画像サイズ
            image.HeightRequest = height - 40;
            image.WidthRequest = height - 40;

            /*
            vertical_desigin.HeightRequest = 400;
            vertical_desigin.WidthRequest = 400;
            button1x.WidthRequest = 0;
            button2x.WidthRequest = 0;
            button3x.WidthRequest = 0;
            button4x.WidthRequest = 0;
            button5x.WidthRequest = 0;
            button6x.WidthRequest = 0;

            horizontal_desigin.HeightRequest = 0;
            horizontal_desigin.WidthRequest = 0;
            button1.WidthRequest = 250;
            button2.WidthRequest = 250;
            button3.WidthRequest = 250;
            button4.WidthRequest = 250;
            button5.WidthRequest = 250;
            button6.WidthRequest = 250;
            */
        }
    }
}
