using System.Text.RegularExpressions;

namespace Alice_Novel;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

	// 初期状態のボタン有効/無効の確認用(nullエラー対策のために初期値はfalseに設定)
	bool button1_start, button2_start, button3_start, button4_start, button5_start, button6_start = false;
	// UI表示/非表示
	bool ui_visible = true;

	private void ReShow_Clicked(object sender, EventArgs e)
	{
		// 画面をクリックしたときの処理
		if (ui_visible == true)
		{
			FileRead();
		}
		else
		{
			// UI再表示処理
			talkname.IsVisible = true;
			textbox.IsVisible = true;
			textbox_out.IsVisible = true;
			// 初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
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
		// button1をクリックしたときの処理
	}

	private void Button2_Clicked(object sender, EventArgs e)
	{
		// button2をクリックしたときの処理

		// 初期のボタン有効/無効状態を確認
		button1_start = button1.IsVisible;
		button2_start = button2.IsVisible;
		button3_start = button3.IsVisible;
		button4_start = button4.IsVisible;
		button5_start = button5.IsVisible;
		button6_start = button6.IsVisible;
		// 画像以外すべて非表示
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
		// button3をクリックしたときの処理
	}

	private void Button4_Clicked(object sender, EventArgs e)
	{
		// button4をクリックしたときの処理
	}

	// .anovファイルを規定
	readonly FilePickerFileType anovFileType = new(
		new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] { ".anov" } },// 拡張子
				{ DevicePlatform.macOS, new[] { "plainText" } },// UTType
				{ DevicePlatform.Android, new[] { "textbox/plain" } },// MIME Type
				{ DevicePlatform.iOS, new[] { "public.plain-text" } },// UTType
				{ DevicePlatform.Tizen, new[] { "*/*" } },
		});

	FileResult result;// .anovファイル選択用
	string FilePath;
	StreamReader sr;
	string sr_read;

	private async void Button5_Clicked(object sender, EventArgs e)
	{
		// button5をクリックしたときの処理

		// .anovファイルを読み込み(もしnullならファイル読み込みを行う)
		result ??= await FilePicker.Default.PickAsync(new PickOptions { 
				PickerTitle = "Alice Novelファイル(.anov)を選択してください。", 
				FileTypes = anovFileType,
			});

		if (result != null)
		{
			FilePath ??= result.FullPath.ToString();

			sr ??= new(FilePath);
			// ファイル読み込み処理
			FileRead();
			game_ui.Title = "Game Title";
			textbox.Text = "";
			talkname.Text = "";
			button5.IsVisible = false;
		}
	}

	void FileRead()
	{
		if (sr != null)
			sr_read = sr.ReadLine();
		if (sr_read != null)
		{
			while (sr_read != "" && sr_read != null)
			{
				string pattern_map = @"> (.*)";// "> "から始まる"場所"を読み込み
				string pattern_chara = @"- (.*)";// "- "から始まる"人物"を読み込み
				string pattern_chara2 = @"- (.*?)/";// "- "から始まって"/ "(感情)が続く場合の"人物"を読み込み
				string pattern_emotion = @"/ (.*)";// "/ "から始まる"感情"を読み込み
				string pattern_talk = @"\[(.*?)\]";// "["と"]"で囲む"会話"を読み込み

				Match match = Regex.Match(sr_read, pattern_map);
				//if (match.Success)
					// 背景変更

				match = Regex.Match(sr_read, pattern_chara);
				if (match.Success)
					talkname.Text = match.Groups[1].Value;

				match = Regex.Match(sr_read, pattern_chara2);
				if (match.Success)
					talkname.Text = match.Groups[1].Value;
					// 感情変更

				match = Regex.Match(sr_read, pattern_emotion);
				//if (match.Success)
					// 感情変更

				match = Regex.Match(sr_read, pattern_talk);
				if (match.Success)
					textbox.Text = match.Groups[1].Value;

				// 次の行を読み込む
				sr_read = sr.ReadLine();
			}
		}
		else
		{
			result = null;
			sr?.Close();
			sr = null;
			talkname.Text = "";
			textbox.Text = "Alice Novelゲーム(.anov)を読み込んでください。";
			button5.IsVisible = true;
			button5.Text = "ロード";
			game_ui.Title = "ゲームをプレイする!";
		}
	}

	private void Button6_Clicked(object sender, EventArgs e)
	{
		// button6をクリックしたときの処理
	}

}
