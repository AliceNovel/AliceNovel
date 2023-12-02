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
			FileRead();
		else
			UI_ReDisplay();
	}

	private async void Button1_Clicked(object sender, EventArgs e)
	{
		// button1をクリックしたときの処理

		// セーブ処理
		if (zip != null)
		{
			ZipArchiveEntry ent = zip.GetEntry(anproj_setting["root-save"] + "savefile.txt");
			ent ??= zip.CreateEntry(anproj_setting["root-save"] + "savefile.txt");
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

		UI_Hidden();
	}

	void UI_Hidden(){
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

	void UI_ReDisplay(){
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
	bool WhileLoading = false;

	// rootの初期値(package.jsonで指定されていない時に使用する値)を設定
	Dictionary<string, string> anproj_setting = [];

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
			FilePath = result.FullPath.ToString();

			read_times = 0;
			// zip内のファイルを読み込み
			zip = ZipFile.Open(FilePath, ZipArchiveMode.Update);

			// zip内のpackage.jsonファイルを読み込み
			ZipArchiveEntry entry = zip.GetEntry("package.json");
			StreamReader sr2 = new(entry.Open(), Encoding.UTF8);
			string str = sr2.ReadToEnd();
			sr2.Close();

			// rootの位置初期値/初期化(package.jsonで指定されていない時に使用する値)を設定
			anproj_setting = new()
			{
				{"root-image", "image/"},
				{"root-background", "image/background/"},
				{"root-story", "story/"},
				{"root-data", "data/"},
				{"root-audio", "audio/"},
				{"root-movie", "movie/"},
				{"root-character", "character.json"},
				{"root-save", "save/"},
				{"first-read", "main.anov"},
				{"game-name", ""},
			};
			// json読み込み
			anproj_setting = JsonToDict(str);
			// タイトルの設定
			game_ui.Title = anproj_setting["game-name"];

			// json読み込み
			static Dictionary<string, string> JsonToDict(string json)
			{
				if (string.IsNullOrEmpty(json))
					return [];
				Dictionary<string, string> dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
				return dict;
			}

			// 最初の.anovファイルを読み込み
			entry = zip.GetEntry(anproj_setting["root-story"] + anproj_setting["first-read"]);

			sr ??= new(entry.Open(), Encoding.UTF8);
			textbox.Text = "";
			talkname.Text = "";
			button5.IsVisible = false;
			
			// セーブ読み込み
			ZipArchiveEntry ent_saveread = zip.GetEntry(anproj_setting["root-save"] + "savefile.txt");
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
							WhileLoading = true;
							for (int i = 1; i < read_loop; i++)
								FileRead();
							// 成功表示
							WhileLoading = false;
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
							using (var st = zip.GetEntry(anproj_setting["root-background"] + match.Groups[1].Value).Open())
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
						ZipArchiveEntry entry = zip.GetEntry(anproj_setting["root-audio"] + match.Groups[1].Value);
						// ファイル保存場所: アプリケーション専用キャッシュフォルダー/match.Groups[1].Value (既存の同名ファイルが存在する場合は上書き保存)
						string temp_audio = Path.GetFullPath(Path.Combine(audio_cache, match.Groups[1].Value));
						if (!Directory.Exists(audio_cache))
							Directory.CreateDirectory(audio_cache);

						entry.ExtractToFile(temp_audio, true);

						audio_bgm.Source = CommunityToolkit.Maui.Views.MediaSource.FromUri(temp_audio);
						audio_bgm.Play();
					}
					catch{}
				}

				// "movie: "から始まる"動画"を読み込み
				match = Regex.Match(sr_read, @"movie: (.*)");
				if (WhileLoading == false)
					if (match.Success)
					{
						// 指定されていない場合は動画を止める
						movie.Stop();
						movie.IsVisible = false;
						// キャッシュ内のすべてのファイルを削除する
						string movie_cache = FileSystem.Current.CacheDirectory;
						try
						{
							DirectoryInfo di = new(movie_cache);
							FileInfo[] files = di.GetFiles();
							foreach (FileInfo file in files)
							{
								file.Delete();
							}
						}
						catch{}

						try
						{
							ZipArchiveEntry entry = zip.GetEntry(anproj_setting["root-movie"] + match.Groups[1].Value);
							// ファイル保存場所: アプリケーション専用キャッシュフォルダー/match.Groups[1].Value (既存の同名ファイルが存在する場合は上書き保存)
							string temp_movie = Path.GetFullPath(Path.Combine(movie_cache, match.Groups[1].Value));
							if (!Directory.Exists(movie_cache))
								Directory.CreateDirectory(movie_cache);

							entry.ExtractToFile(temp_movie, true);

							movie.Source = CommunityToolkit.Maui.Views.MediaSource.FromUri(temp_movie);
							movie.IsVisible = true;
							movie.Play();

							// UI非表示/セリフを進められなくする
							UI_Hidden();
							re.IsEnabled = false;
							// 動画のスキップボタンを実装したら便利そう
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

	private void MovieEnded(object sender, EventArgs e)
	{
		// 動画再生終了時の処理
		Dispatcher.Dispatch(() =>
		{
			// 動画停止
			movie.Stop();
			movie.IsVisible = false;
			// キャッシュ内のすべてのファイルを削除する
			string movie_cache = FileSystem.Current.CacheDirectory;
			try
			{
				DirectoryInfo di = new(movie_cache);
				FileInfo[] files = di.GetFiles();
				foreach (FileInfo file in files)
				{
					file.Delete();
				}
			}
			catch{}

			// UIを元に戻す
			UI_ReDisplay();
			re.IsEnabled = true;
			FileRead();
		});
	}

	private void Button6_Clicked(object sender, EventArgs e)
	{
		// button6をクリックしたときの処理
	}

}
