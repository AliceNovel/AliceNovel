using CommunityToolkit.Maui.Views;
using System.IO.Compression;

namespace AliceNovel.Controls;

internal class AnovReader
{
    public static void ReadPlace(string imagePath, AnprojFormat anprojSettings, ZipArchive zip, Image image, Image bgImage)
    {
        // 場所指定されていない場合は背景画像を消す
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            image.Source = null;
            bgImage.Source = null;
        }
        else if (zip.GetEntry(anprojSettings.RootBackground + imagePath) is not null)
        {
            using (var st = zip.GetEntry(anprojSettings.RootBackground + imagePath).Open())
            {
                var memoryStream = new MemoryStream();
                st.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                bgImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }
    }

    public static void ReadAudio(string audioPath, AnprojFormat anprojSettings, ZipArchive zip, MediaElement mediaElement)
    {
        // 指定されていない場合は音楽を止める
        mediaElement.Stop();

        try
        {
            ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootAudio + audioPath);
            // ファイル保存場所: アプリケーション専用キャッシュフォルダー/音声フォルダ/bgmPath (既存の同名ファイルが存在する場合は上書き保存)
            string audio_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootAudio));
            if (!Directory.Exists(audio_cache))
                Directory.CreateDirectory(audio_cache);

            string temp_audio = Path.GetFullPath(Path.Combine(audio_cache, audioPath));
            if (!File.Exists(temp_audio))
                entry.ExtractToFile(temp_audio, true);

            mediaElement.Source = MediaSource.FromUri(temp_audio);
            mediaElement.Play();
        }
        catch { }
    }

    public static void ReadMovie(string moviePath, AnprojFormat anprojSettings, ZipArchive zip, MediaElement mediaElement)
    {
        // 指定されていない場合は動画を止める
        mediaElement.Stop();
        mediaElement.IsVisible = false;

        try
        {
            ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootMovie + moviePath);
            // ファイル保存場所: アプリケーション専用キャッシュフォルダー/動画フォルダ/moviePath (既存の同名ファイルが存在する場合は上書き保存)
            string movie_cache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootMovie));
            if (!Directory.Exists(movie_cache))
                Directory.CreateDirectory(movie_cache);

            string temp_movie = Path.GetFullPath(Path.Combine(movie_cache, moviePath));
            if (!File.Exists(temp_movie))
                entry.ExtractToFile(temp_movie, true);

            mediaElement.Source = MediaSource.FromUri(temp_movie);
            mediaElement.IsVisible = true;
            mediaElement.Play();

            //// UI非表示/セリフを進められなくする
            // UI_Hidden();
            // re.IsEnabled = false;
            //// 動画のスキップボタンを実装したら便利そう
        }
        catch { }
    }
}
