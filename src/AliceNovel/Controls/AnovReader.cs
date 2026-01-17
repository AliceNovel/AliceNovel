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
                using var memoryStream = new MemoryStream();
                st.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                bgImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }
    }

    public static void ReadAudio(string audioPath, AnprojFormat anprojSettings, ZipArchive zip, MediaElement mediaElement)
    {
        // 指定されていない場合は音楽を止め、指定されている場合も一旦止める
        mediaElement.Stop();

        ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootAudio + audioPath);
        if (entry is null)
            return;

        // ファイル保存場所: アプリケーション専用キャッシュフォルダー/音声フォルダ/bgmPath (既存の同名ファイルが存在する場合は上書き保存)
        string audioCache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootAudio));
        if (!Directory.Exists(audioCache))
            Directory.CreateDirectory(audioCache);

        string tempAudio = Path.GetFullPath(Path.Combine(audioCache, audioPath));
        if (!File.Exists(tempAudio))
            entry.ExtractToFile(tempAudio, true);

        mediaElement.Source = MediaSource.FromUri(tempAudio);
        mediaElement.Play();
    }

    public static bool ReadMovie(string moviePath, AnprojFormat anprojSettings, ZipArchive zip, MediaElement mediaElement)
    {
        // 指定されていない場合は動画を止め、指定されている場合も一旦止める
        mediaElement.Stop();
        mediaElement.IsVisible = false;

        ZipArchiveEntry entry = zip.GetEntry(anprojSettings.RootMovie + moviePath);
        if (entry is null)
            return false;

        // ファイル保存場所: アプリケーション専用キャッシュフォルダー/動画フォルダ/moviePath (既存の同名ファイルが存在する場合は上書き保存)
        string movieCache = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, anprojSettings.RootMovie));
        if (!Directory.Exists(movieCache))
            Directory.CreateDirectory(movieCache);

        string tempMovie = Path.GetFullPath(Path.Combine(movieCache, moviePath));
        if (!File.Exists(tempMovie))
            entry.ExtractToFile(tempMovie, true);

        mediaElement.Source = MediaSource.FromUri(tempMovie);
        mediaElement.IsVisible = true;
        mediaElement.Play();

        // 動画のスキップボタンを実装したら便利そう
        return true;
    }
}
