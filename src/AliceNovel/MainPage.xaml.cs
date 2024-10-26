using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.IO.Compression;
using AliceNovel.Resources.Strings;
// using CommunityToolkit.Maui.Alerts;

namespace AliceNovel;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		// 初期時の表示文字を保存
		Initial_textbox_text = textbox.Text;
		Initial_button5_text = button5.Text;
		Initial_game_title = game_ui.Title;
	}

	// 初期状態のボタン有効/無効の確認用
	bool Initial_button1, Initial_button2, Initial_button3, Initial_button4, Initial_button5, Initial_button6;
	// 初期状態で表示されている文字
	readonly string Initial_textbox_text, Initial_button5_text, Initial_game_title;

	// UI表示/非表示
	bool ui_visible = true;

	/// <summary>
	/// 画面をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ReShow_Clicked(object sender, EventArgs e)
	{
		if (ui_visible == true)
			FileRead();
		else
			UI_ReDisplay();
	}

	/// <summary>
	/// button1 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button1_Clicked(object sender, EventArgs e)
	{
		FileSave();
	}

	/// <summary>
	/// セーブ処理です。
	/// </summary>
	async void FileSave(){
		if (zip != null)
		{
			ZipArchiveEntry ent = zip.GetEntry(anproj_setting["root-save"] + "savefile.txt");
			ent ??= zip.CreateEntry(anproj_setting["root-save"] + "savefile.txt");
			using (StreamWriter sw = new(ent.Open()))
			{
				sw.WriteLine(read_times);
			}
			// 成功表示
			await DisplayAlert("セーブ", "セーブが成功しました。", "OK");
		}
	}

	/// <summary>
	/// button2 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button2_Clicked(object sender, EventArgs e)
	{
		UI_Hidden();
	}

	/// <summary>
	/// 画像をフル画面で閲覧するために UI を隠します。
	/// </summary>
	void UI_Hidden(){
		// 初期のボタン有効/無効状態を確認
		Initial_button1 = button1.IsVisible;
		Initial_button2 = button2.IsVisible;
		Initial_button3 = button3.IsVisible;
		Initial_button4 = button4.IsVisible;
		Initial_button5 = button5.IsVisible;
		Initial_button6 = button6.IsVisible;
		// 画像以外すべて非表示
		talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = ui_visible = false;
		button1.IsVisible = button2.IsVisible = button3.IsVisible = button4.IsVisible = button5.IsVisible = button6.IsVisible = false;
	}

	/// <summary>
	/// 画像をフル画面で閲覧するために非表示した UI を再表示します。
	/// </summary>
	void UI_ReDisplay(){
		talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = ui_visible = true;
		// 初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
		button1.IsVisible = Initial_button1;
		button2.IsVisible = Initial_button2;
		button3.IsVisible = Initial_button3;
		button4.IsVisible = Initial_button4;
		button5.IsVisible = Initial_button5;
		button6.IsVisible = Initial_button6;
	}

	/// <summary>
	/// button3 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button3_Clicked(object sender, EventArgs e)
	{
		
	}

	/// <summary>
	/// button4 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button4_Clicked(object sender, EventArgs e)
	{
		
	}

	/// <summary>
	/// .anproj ファイルの規定です。
	/// </summary>
	readonly FilePickerFileType anprojFileType = new(
		new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] { ".anproj" } },// 拡張子
			{ DevicePlatform.macOS, new[] { "archive", ".anproj" } },// UTType
			{ DevicePlatform.Android, new[] { "application/octet-stream", ".anproj" } },// MIME Type
			{ DevicePlatform.iOS, new[] { "public.archive", ".anproj" } },// UTType
			{ DevicePlatform.Tizen, new[] { "*/*", ".anproj" } },
		});

	FileResult result;// .anprojファイル選択用
	StreamReader sr;
	string sr_read;
	ZipArchive zip;
	bool WhileLoading = false;

	// rootの初期値(package.jsonで指定されていない時に使用する値)を設定
	Dictionary<string, string> anproj_setting = [];

	int read_times = 0;// 読み込み回数(セーブ用)

	/// <summary>
	/// button5 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void Button5_Clicked(object sender, EventArgs e)
	{
		// キャッシュフォルダを削除する
		string path = FileSystem.Current.CacheDirectory;
		if (Directory.Exists(path))
			Directory.Delete(path, true);

		// .anprojファイルを読み込み(もしnullならファイル読み込みを行う)
		result ??= await FilePicker.Default.PickAsync(new PickOptions { 
				PickerTitle = AppResources.TextBox__Default_,
				FileTypes = anprojFileType,
				});

		if (result == null)
			return;

		read_times = 0;
		// zip内のファイルを読み込み
		zip = ZipFile.Open(result.FullPath.ToString(), ZipArchiveMode.Update);

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
		// json を読み込み、デフォルト設定に上書き
		if (!string.IsNullOrEmpty(str))
			foreach (var key in JsonSerializer.Deserialize<Dictionary<string, string>>(str))
			{
				if (anproj_setting.ContainsKey(key.Key))
					anproj_setting[key.Key] = key.Value;
			}
		
		// 最初の .anov ファイルを読み込み
		if (zip.GetEntry(anproj_setting["root-story"] + anproj_setting["first-read"]) != null)
			entry = zip.GetEntry(anproj_setting["root-story"] + anproj_setting["first-read"]);
		// (v0.9.0-rc1 の互換性のため) (画像はディレクトリや設定形式が異なるので、現状は非対応)
		else if (zip.GetEntry(anproj_setting["first-read"]) != null)
			entry = zip.GetEntry(anproj_setting["first-read"]);
		else
		{
			await DisplayAlert("警告", "ファイルが古い形式で、対応していません。", "OK");
			return;
		}

		// タイトルの設定
		game_ui.Title = anproj_setting["game-name"];

		sr ??= new(entry.Open(), Encoding.UTF8);
		textbox.Text = "";
		talkname.Text = "";
		button1.IsVisible = true;
		button2.IsVisible = true;
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
						// ここは DisplayAlert ではなく CommunityToolkit.Maui.Alerts の Toast がいいが、現状 Windows (.exe) 上でエラーになる
						// await Toast.Make("ロードが成功しました。").Show();
					}
					catch
					{
						// 失敗表示
						await DisplayAlert("警告", "ロードが失敗したため、最初から読み込みを行います。", "OK");
					}
				}
				srz.Dispose();
			}
			catch { }
		}

		// 初回ファイル読み込み処理
		FileRead();
	}

	/// <summary>
	/// .anproj ファイルを読み込みます。
	/// </summary>
	void FileRead()
	{
		read_times++;
		if (sr != null)
			sr_read = sr.ReadLine();
		if (sr_read != null)
		{
			while (sr_read != "" && sr_read != null)
			{
				Match match;

				// "["と"]"で囲む"会話"を読み込み
				match = Regex.Match(sr_read, @"\[(.*?)\]");
				if (match.Success)
				{
					textbox.Text = match.Groups[1].Value.Trim();
					sr_read = sr.ReadLine(); // 次の行を読み込む
					continue; // 上から再開
				}

				// "> "から始まる"場所"を読み込み
				match = Regex.Match(sr_read, @"> (.*)");
				if (match.Success)
				{
					// 場所指定されていない場合は背景画像を消す
					if (match.Groups[1].Value.Trim() == "")
						image.Source = null;
					else if (zip.GetEntry(anproj_setting["root-background"] + match.Groups[1].Value.Trim()) is not null)
						{
							using (var st = zip.GetEntry(anproj_setting["root-background"] + match.Groups[1].Value.Trim()).Open())
							{
								var memoryStream = new MemoryStream();
								st.CopyTo(memoryStream);
								memoryStream.Seek(0, SeekOrigin.Begin);
								image.Source = ImageSource.FromStream(() => memoryStream);
							}
						}
				}

				// "bgm: "から始まる"音楽"を読み込み
				match = Regex.Match(sr_read, @"bgm: (.*)");
				if (match.Success)
				{
					// 指定されていない場合は音楽を止める
					audio_bgm.Stop();

					try
					{
						ZipArchiveEntry entry = zip.GetEntry(anproj_setting["root-audio"] + match.Groups[1].Value.Trim());
						// ファイル保存場所: アプリケーション専用キャッシュフォルダー/音声フォルダ/match.Groups[1].Value.Trim() (既存の同名ファイルが存在する場合は上書き保存)
						string audio_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anproj_setting["root-audio"]));
						if (!Directory.Exists(audio_cache))
							Directory.CreateDirectory(audio_cache);

						string temp_audio = Path.GetFullPath(Path.Combine(audio_cache, match.Groups[1].Value.Trim()));
						if (!File.Exists(temp_audio))
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

						try
						{
							ZipArchiveEntry entry = zip.GetEntry(anproj_setting["root-movie"] + match.Groups[1].Value.Trim());
							// ファイル保存場所: アプリケーション専用キャッシュフォルダー/動画フォルダ/match.Groups[1].Value.Trim() (既存の同名ファイルが存在する場合は上書き保存)
							string movie_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anproj_setting["root-movie"]));
							if (!Directory.Exists(movie_cache))
								Directory.CreateDirectory(movie_cache);

							string temp_movie = Path.GetFullPath(Path.Combine(movie_cache, match.Groups[1].Value.Trim()));
							if (!File.Exists(temp_movie))
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
					talkname.Text = match.Groups[1].Value.Trim();

				// "- "から始まって"/ "が続く場合の"人物"と"感情"を読み込み
				match = Regex.Match(sr_read, @"- (.*?)/");
				if (match.Success)
					talkname.Text = match.Groups[1].Value.Trim();
					// 感情変更

				// "/ "から始まる"感情"を読み込み
				match = Regex.Match(sr_read, @"/ (.*)");
				//if (match.Success)
					// 感情変更

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
			textbox.Text = Initial_textbox_text;
			button1.IsVisible = false;
			button2.IsVisible = false;
			button5.IsVisible = true;
			button5.Text = Initial_button5_text;
			game_ui.Title = Initial_game_title;

			// キャッシュフォルダを削除する
			string path = FileSystem.Current.CacheDirectory;
			if (Directory.Exists(path))
				Directory.Delete(path, true);
		}
	}

	/// <summary>
	/// 動画再生終了時の処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void MovieEnded(object sender, EventArgs e)
	{
		Dispatcher.Dispatch(() =>
		{
			// 動画停止
			movie.Stop();
			movie.IsVisible = false;

			// UIを元に戻す
			UI_ReDisplay();
			re.IsEnabled = true;
			FileRead();
		});
	}

	/// <summary>
	/// button6 をクリックしたときの処理です。
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button6_Clicked(object sender, EventArgs e)
	{
		
	}

}
