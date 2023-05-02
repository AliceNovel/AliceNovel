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
        }
		else
		{

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
        }
    }
}
