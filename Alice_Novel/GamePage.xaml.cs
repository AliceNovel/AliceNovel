using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.IO.Compression;
using CommunityToolkit.Maui.Alerts;

namespace Alice_Novel;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

	// 初期状態のボタン有効/無効の確認用(nullエラー対策のために初期値はfalseに設定)
	bool button1_start = false, button2_start = false, button3_start = false, button4_start = false, button5_start = false, button6_start = false;
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
			talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = ui_visible = true;
			// 初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
			button1.IsVisible = button1_start;
			button2.IsVisible = button2_start;
			button3.IsVisible = button3_start;
			button4.IsVisible = button4_start;
			button5.IsVisible = button5_start;
			button6.IsVisible = button6_start;
		}
	}

	private async void Button1_Clicked(object sender, EventArgs e)
	{
		// button1をクリックしたときの処理

		// セーブ処理
		if (zip != null)
		{
			ZipArchiveEntry ent = zip.GetEntry(root_save + "savefile.txt");
			ent ??= zip.CreateEntry(root_save + "savefile.txt");
			using (StreamWriter sw = new(ent.Open()))
			{
				sw.WriteLine(read_times);
			}
			// 成功表示
			await Toast.Make("セーブが成功しました。").Show();
		}
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
		button1.IsVisible = button2.IsVisible = button3.IsVisible = button4.IsVisible = button5.IsVisible = button6.IsVisible = false;
		talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = ui_visible = false;
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

	string root_image = "", root_background = "";// image
	string root_audio = "";// audio
	string root_story = "", first_read = "";// story
	string root_data = "", root_character = "", root_save = "";// data

	int read_times = 0;// 読み込み回数(セーブ用)

	private async void Button5_Clicked(object sender, EventArgs e)
	{
		// button5をクリックしたときの処理

		// .anprojファイルを読み込み(もしnullならファイル読み込みを行う)
		result ??= await FilePicker.Default.PickAsync(new PickOptions { 
				PickerTitle = "Alice Novelゲームを読み込んでください。", 
				FileTypes = anprojFileType,
				});

		if (result != null)
		{
			FilePath ??= result.FullPath.ToString();

			read_times = 0;
			// zip内のファイルを読み込み
			zip = ZipFile.Open(FilePath, ZipArchiveMode.Update);

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
			root_character = "character.json";
			root_save = "save/";
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

					case "root-character":
						root_character = dict[key];
						break;

					case "root-save":
						root_save = dict[key];
						break;

					default:
						break;
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

			// 最初の.anovファイルを読み込み
			entry = zip.GetEntry(root_story + first_read);

			sr ??= new(entry.Open(), Encoding.UTF8);
			textbox.Text = "";
			talkname.Text = "";
			button5.IsVisible = false;
			
			// セーブ読み込み
			ZipArchiveEntry ent_saveread = zip.GetEntry(root_save + "savefile.txt");
			if (ent_saveread != null)
			{
				try
				{
					StreamReader srz = new(ent_saveread.Open());
					int read_loop = int.Parse(srz.ReadToEnd());

					bool answer = await DisplayAlert("セーブデータが見つかりました。", "セーブデータをロードしますか?", "ロードする", "はじめから");
					if (answer == true)
					{
						// "セーブデータをロード"を選択した場合のみ、この処理を実行
						try
						{
							for (int i = 1; i < read_loop; i++)
								FileRead();
							// 成功表示
							await Toast.Make("ロードが成功しました。").Show();
						}
						catch
						{
							// 失敗表示
							await Toast.Make("ロードが失敗したため、最初から読み込みを行います。").Show();
						}
					}
					srz.Dispose();
				}
				catch { }
			}

            // 初回ファイル読み込み処理
            FileRead();
		}
	}

	void FileRead()
	{
		read_times++;
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
					// 場所指定されていない場合は背景画像を消す
					if (match.Groups[1].Value == "")
					{
						image.Source = null;
					}
					else
					{
						try
						{
							using (var st = zip.GetEntry(root_background + match.Groups[1].Value).Open())
							{
								var memoryStream = new MemoryStream();
								st.CopyTo(memoryStream);
								memoryStream.Seek(0, SeekOrigin.Begin);
								image.Source = ImageSource.FromStream(() => memoryStream);
							}
						}
						catch { }
					}
				}

				// "bgm: "から始まる"音楽"を読み込み
				match = Regex.Match(sr_read, @"bgm: (.*)");
				if (match.Success)
				{
					// 指定されていない場合は音楽を止める
					audio_bgm.Stop();
					// キャッシュ内のすべてのファイルを削除する
					string audio_cache = FileSystem.Current.CacheDirectory;
					try
					{
						DirectoryInfo di = new(audio_cache);
						FileInfo[] files = di.GetFiles();
						foreach (FileInfo file in files)
						{
							file.Delete();
						}
					}
					catch{}

					try
					{
						ZipArchiveEntry entry = zip.GetEntry(root_audio + match.Groups[1].Value);
						// ファイル保存場所: アプリケーション専用キャッシュフォルダー/match.Groups[1].Value (既存の同名ファイルが存在する場合は上書き保存)
						string temp_audio = Path.GetFullPath(Path.Combine(audio_cache, match.Groups[1].Value));
						if (!System.IO.Directory.Exists(audio_cache))
							Directory.CreateDirectory(audio_cache);

						entry.ExtractToFile(temp_audio, true);

						audio_bgm.Source = CommunityToolkit.Maui.Views.MediaSource.FromUri(temp_audio);
						audio_bgm.Play();
					}
					catch{}
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
			textbox.Text = "Alice Novelゲームを読み込んでください。";
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
