using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.IO.Compression;

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

	// .anprojファイルを規定
	readonly FilePickerFileType anprojFileType = new(
		new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] { ".anproj" } },// 拡張子
			{ DevicePlatform.macOS, new[] { "archive", ".anproj" } },// UTType
			{ DevicePlatform.Android, new[] { "application/x-freearc", ".anproj" } },// MIME Type
			{ DevicePlatform.iOS, new[] { "public.archive", ".anproj" } },// UTType
			{ DevicePlatform.Tizen, new[] { "*/*", ".anproj" } },
		});

	FileResult result;// .anprojファイル選択用
	string FilePath;
	StreamReader sr;
	string sr_read;
	ZipArchive zip;

	string root_image, root_background = "";// image
	string root_audio = "";// audio
	string root_story, first_read = "";// story
	string root_data, root_place, root_character = "";// data

	private async void Button5_Clicked(object sender, EventArgs e)
	{
		// button5をクリックしたときの処理

		// .anprojファイルを読み込み(もしnullならファイル読み込みを行う)
		result ??= await FilePicker.Default.PickAsync(new PickOptions { 
				PickerTitle = "Alice Novelファイル(.anproj)を選択してください。", 
				FileTypes = anprojFileType,
				});

		if (result != null)
		{
			FilePath ??= result.FullPath.ToString();

			// zip内のファイルを読み込み
			zip = ZipFile.OpenRead(FilePath);

			// zip内のpackage.jsonファイルを読み込み
			ZipArchiveEntry entry = zip.GetEntry("package.json");
			StreamReader sr2 = new(entry.Open(), Encoding.UTF8);
			string str = sr2.ReadToEnd();
			sr2.Close();
			var dict = JsonToDict(str);
			// rootの初期値(package.jsonで指定されていない時に使用する値)を設定
			root_image = "image/";
			root_background = "image/background/";
			root_story = "story/";
			root_data = "data/";
			root_audio = "audio/";
			root_place = "place.json";
			root_character = "character.json";
			first_read = "main.anov";
			// package.jsonでゲームタイトルが指定されていない時は空欄にする
			game_ui.Title = "";
			// json処理
			foreach (string key in dict.Keys)
			{
				switch (key)
				{
					case "game-name":
						game_ui.Title = dict[key];
						break;

					case "first-read":
						first_read = dict[key];
						break;

					case "root-image":
						root_image = dict[key];
						break;

					case "root-background":
						root_background = dict[key];
						break;

					case "root-story":
						root_story = dict[key];
						break;

					case "root-data":
						root_data = dict[key];
						break;

					case "root-audio":
						root_audio = dict[key];
						break;

					case "root-place":
						root_place = dict[key];
						break;

					case "root-character":
						root_character = dict[key];
						break;

					default:
						break;
				}
			}

			// 最初の.anovファイルを読み込み
			entry = zip.GetEntry(root_story + first_read);

			sr ??= new(entry.Open(), Encoding.UTF8);
			textbox.Text = "";
			talkname.Text = "";
			// ファイル読み込み処理
			FileRead();
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
				// "> "から始まる"場所"を読み込み
				Match match = Regex.Match(sr_read, @"> (.*)");
				if (match.Success)
				{
					try
					{
						string image_place = "";
						StreamReader sr3 = new(zip.GetEntry(root_data + root_place).Open(), Encoding.UTF8);
						string str = sr3.ReadToEnd();
						sr3.Close();
						var dict = JsonToDict(str);
						// json処理
						foreach (string key in dict.Keys)
						{
							if (key == match.Groups[1].Value)
								image_place = dict[key];
						}

						using (var st = zip.GetEntry(root_background + image_place).Open())
						{
							var memoryStream = new MemoryStream();
							st.CopyTo(memoryStream);
							memoryStream.Seek(0, SeekOrigin.Begin);
							image.Source = ImageSource.FromStream(() => memoryStream);
						}
					}
					catch{}

					// 場所指定されていない場合は背景画像を消す
					if (match.Groups[1].Value == "")
						image.Source = null;
				}

				// "- "から始まる"人物"を読み込み
				match = Regex.Match(sr_read, @"- (.*)");
				if (match.Success)
					talkname.Text = match.Groups[1].Value;

				// "- "から始まって"/ "が続く場合の"人物"と"感情"を読み込み
				match = Regex.Match(sr_read, @"- (.*?)/");
				if (match.Success)
					talkname.Text = match.Groups[1].Value;
					// 感情変更

				// "/ "から始まる"感情"を読み込み
				match = Regex.Match(sr_read, @"/ (.*)");
				//if (match.Success)
					// 感情変更

				// "["と"]"で囲む"会話"を読み込み
				match = Regex.Match(sr_read, @"\[(.*?)\]");
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
			zip?.Dispose();// zipファイルを閉じる
			talkname.Text = "";
			image.Source = null;
			textbox.Text = "Alice Novelゲーム(.anproj)を読み込んでください。";
			button5.IsVisible = true;
			button5.Text = "ロード";
			game_ui.Title = "ゲームをプレイする!";
		}
	}

	// json読み込み
	static Dictionary<string, string> JsonToDict(string json)
	{
		if (String.IsNullOrEmpty(json))
			return new Dictionary<string, string>();
		Dictionary<string, string> dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
		return dict;
	}

	private void Button6_Clicked(object sender, EventArgs e)
	{
		// button6をクリックしたときの処理
	}

}
