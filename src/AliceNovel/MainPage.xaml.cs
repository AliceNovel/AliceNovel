using AliceNovel.Controls;
using AliceNovel.Resources.Strings;
using Microsoft.Maui.Controls.StyleSheets;
using System.IO.Compression;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace AliceNovel;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.Android)
            re.GestureRecognizers.Clear();
    }

    // 初期状態のボタン有効/無効の確認用
    private bool[] initialButtonsState = new bool[6];

    // UI表示/非表示
    bool currentUIVisible = true;

    /// <summary>
    /// 画面をクリックしたときの処理です。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReShow_Clicked(object sender, EventArgs e)
    {
        if (currentUIVisible)
            FileRead();
        else
            UI_ReDisplay();
    }

    /// <summary>
    /// Hide Interface
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        if (currentUIVisible)
            UI_Hidden();
        else
            UI_ReDisplay();
    }

    /// <summary>
    /// Save the Game
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        FileSave();
    }

    /// <summary>
    /// Exit the Game
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async private void ToolbarItem_Clicked_3(object sender, EventArgs e)
    {
        bool answer = await DisplayAlertAsync(AppResources.ToolbarItem3__Exit_, AppResources.ToolbarItem3__Save_or_not_, AppResources.ToolbarItem3__Save_and_Exit_, AppResources.ToolbarItem3__only_Exit_);
        if (answer)
            FileSave();
        ExitGame();
    }

    /// <summary>
    /// The function when droped .anproj file into this app.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #pragma warning disable IDE0051 // 警告を非表示: 使用されていないプライベート メンバーを削除する (この警告が表示されるのは、多分 Visual Studio のバグ)
    async private void OnDrop(object sender, DropEventArgs e)
    #pragma warning restore IDE0051 // 警告を非表示: 使用されていないプライベート メンバーを削除する
    {
        string filePath = await FileDroper.DropAsync(e);
        if (string.IsNullOrEmpty(filePath))
            return;

        // Process the dropped file
        // [Want to] Activate current Window
        // Check whether open the .anproj file or not
        bool answer = await DisplayAlertAsync(AppResources.Alert__Confirmation_, AppResources.Alert__FileDrop_ + "<"+ filePath + ">", AppResources.Alert__Confirm_, AppResources.Alert__Canncel_);
        if (answer != true)
            return;

        FirstFileReader(filePath);
    }

    readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true,
    };

    /// <summary>
    /// セーブ処理です。
    /// </summary>
    async void FileSave(){
        if (zip is null)
            return;

        // 保存するデータ
        List<SaveDataInfo.SaveDataLists> saveDataLists = [];
        saveDataLists.Add(new SaveDataInfo.SaveDataLists {
            CurrentLines = read_times,
            LastUpdated = DateTimeOffset.Now,
        });
        SaveDataInfo saveValues = new()
        {
            GameTitle = anprojSettings.GameName,
            GameEngine = AppInfo.Current.Name,
            EngineVersion = AppInfo.Current.VersionString,
            SaveLists = saveDataLists,
        };

        string writerInfo = JsonSerializer.Serialize(saveValues, jsonOptions);

        // .anproj 内保存
        ZipArchiveEntry ent = zip.GetEntry(anprojSettings.RootSave + SAVE_JSON_NAME);
        ent ??= zip.CreateEntry(anprojSettings.RootSave + SAVE_JSON_NAME);
        using (StreamWriter sw = new(ent.Open()))
        {
            sw.WriteLine(writerInfo);
        }

        // ローカル保存
        string localSaveDirectory = Path.Combine(FileSystem.Current.AppDataDirectory, "SaveData", anprojSettings.GameName);
        // (保存先のディレクトリ作成)
        if (!Directory.Exists(localSaveDirectory))
            Directory.CreateDirectory(localSaveDirectory);
        string localSaveFile = Path.Combine(localSaveDirectory, SAVE_JSON_NAME);
        using (StreamWriter sw = new(File.Create(localSaveFile)))
        {
            sw.WriteLine(writerInfo);
        }

        // 成功表示
        await DisplayAlertAsync(AppResources.Alert__Save1_, AppResources.Alert__Save2_, AppResources.Alert__Confirm_);
    }

    /// <summary>
    /// 画像をフル画面で閲覧するために UI を隠します。
    /// </summary>
    void UI_Hidden(){
        toolbarItem1.Text = AppResources.ToolbarItem1__Reshow_;

        // 初期のボタン有効/無効状態を確認
        initialButtonsState[4] = button5.IsVisible;
        initialButtonsState[5] = button6.IsVisible;
        // 画像以外すべて非表示
        talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = textboxRoundBorder.IsVisible = currentUIVisible = false;
        button5.IsVisible = button6.IsVisible = false;
    }

    /// <summary>
    /// 画像をフル画面で閲覧するために非表示した UI を再表示します。
    /// </summary>
    void UI_ReDisplay(){
        toolbarItem1.Text = AppResources.Button2;

        talkname.IsVisible = textbox.IsVisible = textbox_out.IsVisible = textboxRoundBorder.IsVisible = currentUIVisible = true;
        // 初期値に設定(初期で表示されていたら表示、そうでなかったら非表示)
        button5.IsVisible = initialButtonsState[4];
        button6.IsVisible = initialButtonsState[5];
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
    bool readCss = false;

    // ゲームの設定を保持する変数
    AnprojFormat anprojSettings = new();
    readonly string SAVE_JSON_NAME = "savefile.json";

    int read_times = 0;// 読み込み回数(セーブ用)

    /// <summary>
    /// button5 をクリックしたときの処理です。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Button5_Clicked(object sender, EventArgs e)
    {
        // .anprojファイルを読み込み(もしnullならファイル読み込みを行う)
        result ??= await FilePicker.Default.PickAsync(new PickOptions { 
                PickerTitle = AppResources.TextBox__Default_,
                FileTypes = anprojFileType,
                });

        if (result is null)
            return;

        FirstFileReader(result.FullPath.ToString());
    }

    /// <summary>
    /// .anproj ファイルの初回読み込みの処理を行います。
    /// </summary>
    /// <param name="targetFilePath">.anproj ファイルのパス</param>
    async void FirstFileReader(string targetFilePath)
    {
        // キャッシュファイルを削除する
        FileLoader.ClearCache();

        read_times = 0;
        // zip内のファイルを読み込み
        zip = ZipFile.Open(targetFilePath, ZipArchiveMode.Update);

        // zip内のpackage.jsonファイルを読み込み
        ZipArchiveEntry entry = zip.GetEntry("package.json");
        StreamReader sr2 = new(entry.Open(), Encoding.UTF8);
        string str = sr2.ReadToEnd();
        sr2.Close();

        if (!string.IsNullOrEmpty(str))
            // json を読み込み、デフォルト設定に上書き
            anprojSettings = JsonSerializer.Deserialize<AnprojFormat>(str, jsonOptions);
        else
            // rootの位置初期値/初期化(package.jsonで指定されていない時に使用する値)を設定
            anprojSettings = new();

        // 最初の .anov ファイルを読み込み
        try
        {
            entry = zip.GetEntry(anprojSettings.RootStory + anprojSettings.FirstRead);
        }
        catch
        {
            await DisplayAlertAsync(AppResources.Alert__Warn1_, AppResources.Alert__Warn2_, AppResources.Alert__Confirm_);
            return;
        }

        // タイトルの設定
        game_ui.Title = anprojSettings.GameName;

        sr ??= new(entry.Open(), Encoding.UTF8);
        textbox.Text = "";
        talkname.Text = "";
        button5.IsVisible = false;
        toolbarItem1.IsEnabled = true;
        toolbarItem2.IsEnabled = true;
        toolbarItem3.IsEnabled = true;

        // CSS の読み込み
        ZipArchiveEntry cssZip = zip.GetEntry(anprojSettings.Style);
        if (cssZip is not null)
        {
            StreamReader r = null;
            try
            {
                r = new(cssZip.Open());
                Resources.Add(StyleSheet.FromReader(r));
                readCss = true;
            }
            finally
            {
                r?.Dispose();
            }
        }

        // セーブ読み込み
        // 現状は .anproj 内のセーブデータを優先、なければローカルデータを参照する
        // .anproj 内のデータから読み込み
        ZipArchiveEntry ent_saveread = zip.GetEntry(anprojSettings.RootSave + SAVE_JSON_NAME);
        if (ent_saveread is not null)
        {
            StreamReader srz = null;
            try
            {
                srz = new(ent_saveread.Open());
                LoadSaveOrNot(srz.ReadToEnd());
            }
            finally
            {
                srz?.Dispose();
            }
        }
        // ローカルデータから読み込み
        else
        {
            try
            {
                string localSaveData = File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "SaveData", anprojSettings.GameName, SAVE_JSON_NAME));
                LoadSaveOrNot(localSaveData);
            }
            catch { }
        }

        async void LoadSaveOrNot(string saveData)
        {
            if (string.IsNullOrEmpty(saveData))
                return;

            int read_loop;
            try
            {
                SaveDataInfo loadData = JsonSerializer.Deserialize<SaveDataInfo>(saveData, jsonOptions);
                read_loop = loadData.SaveLists.FirstOrDefault().CurrentLines;
            }
            catch
            {
                await DisplayAlertAsync(AppResources.Alert__Warn1_, AppResources.Alert__Load5_, AppResources.Alert__Confirm_);
                return;
            }

            bool answer = await DisplayAlertAsync(AppResources.Alert__Load1_, AppResources.Alert__Load2_, AppResources.Alert__Load3_, AppResources.Alert__Load4_);
            if (answer != true)
                return;

            WhileLoading = true;
            // "セーブデータをロード"を選択した場合のみ、この処理を実行
            try
            {
                for (int i = 1; i < read_loop; i++)
                    FileRead();
            }
            catch
            {
                // 失敗表示
                await DisplayAlertAsync(AppResources.Alert__Warn1_, AppResources.Alert__Load5_, AppResources.Alert__Confirm_);
            }
            WhileLoading = false;
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
        sr_read = sr?.ReadLine();
        if (sr_read is null)
        {
            ExitGame();
            return;
        }

        while (sr_read != "" && sr_read is not null)
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
                {
                    image.Source = null;
                    bgImage.Source = null;
                }
                else if (zip.GetEntry(anprojSettings.RootBackground + match.Groups[1].Value.Trim()) is not null)
                {
                    using (var st = zip.GetEntry(anprojSettings.RootBackground + match.Groups[1].Value.Trim()).Open())
                    {
                        var memoryStream = new MemoryStream();
                        st.CopyTo(memoryStream);
                        byte[] bytes = memoryStream.ToArray();
                        image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                        bgImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
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
                    ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootAudio + match.Groups[1].Value.Trim());
                    // ファイル保存場所: アプリケーション専用キャッシュフォルダー/音声フォルダ/match.Groups[1].Value.Trim() (既存の同名ファイルが存在する場合は上書き保存)
                    string audio_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootAudio));
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
            if (!WhileLoading && match.Success)
            {
                // 指定されていない場合は動画を止める
                movie.Stop();
                movie.IsVisible = false;

                try
                {
                    ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootMovie + match.Groups[1].Value.Trim());
                    // ファイル保存場所: アプリケーション専用キャッシュフォルダー/動画フォルダ/match.Groups[1].Value.Trim() (既存の同名ファイルが存在する場合は上書き保存)
                    string movie_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootMovie));
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

            // Captures the people name and its emotion
            // "- example" → people: "example", emotion: null
            // "- example / happy" → people: "example", emotion: "happy"
            // "/ happy" → people: null, emotion: "happy"
            string pattern = @"^(?:-\s*(?<people>[^/]+))?(?:\s*/\s*(?<emotion>.+))?$";
            match = Regex.Match(sr_read, pattern);
            if (match.Groups["people"].Success)
            {
                string people = match.Groups["people"].Value.Trim();

                if (!string.IsNullOrWhiteSpace(people))
                    talkname.Text = people;
            }
            if (match.Groups["emotion"].Success)
            {
                string emotion = match.Groups["emotion"].Value.Trim();
                // if (!string.IsNullOrWhiteSpace(emotion))
                // [ToDo] Change emotion
            }

            // 次の行を読み込む
            sr_read = sr.ReadLine();
        }
    }

    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    private void ExitGame()
    {
        // 動画停止処理
        movie.Stop();
        movie.IsVisible = false;
        UI_ReDisplay();
        re.IsEnabled = true;

        // CSS のリセットのための再起動
        FileLoader.CssReset(readCss);

        result = null;
        sr?.Close();
        sr = null;
        zip?.Dispose();// zipファイルを閉じる
        talkname.Text = "";
        image.Source = null;
        bgImage.Source = null;
        textbox.Text = AppResources.TextBox__Default_;
        button5.IsVisible = true;
        button5.Text = AppResources.Button5;
        game_ui.Title = AppResources.MainPage_Title;
        toolbarItem1.IsEnabled = false;
        toolbarItem2.IsEnabled = false;
        toolbarItem3.IsEnabled = false;

        // キャッシュフォルダを削除する
        FileLoader.ClearCache();
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

            // 手動メモリ解放
            GC.Collect();

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
