using CommunityToolkit.Maui.Views;
using System.IO.Compression;

namespace AliceNovel.Controls;

internal class AnovReader
{
    public static void ReadPlace(string imagePath, AnprojFormat anprojSettings, ZipArchive zip, Image image, Image bgImage)
    {
        // 場所指定されていない場合・背景画像が読み込めなかった時のために、背景画像を消す
        image.Source = null;
        bgImage.Source = null;

        if (zip.GetEntry(anprojSettings.RootBackground + imagePath) is not null)
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

    /// <summary>
    /// Read audio or movie from anproj zip archive
    /// </summary>
    /// <param name="mediaPath">The audio or movie path</param>
    /// <param name="anprojSettings">Anproj-Format settings</param>
    /// <param name="zip">Zip Archive</param>
    /// <param name="mediaElement">Media element, it uses</param>
    /// <param name="isMovie">true: movie, false: audio</param>
    /// <returns>success: true, failure: false</returns>
    public static bool ReadMedia(string mediaPath, AnprojFormat anprojSettings, ZipArchive zip, MediaElement mediaElement, bool isMovie = true)
    {
        // 指定されていない場合は動画を止め、指定されている場合も一旦止める
        mediaElement.Stop();
        // 動画の場合、非表示にする
        if (isMovie)
            mediaElement.IsVisible = false;

        string root = isMovie ? anprojSettings.RootMovie : anprojSettings.RootAudio;

        ZipArchiveEntry entry = zip.GetEntry(root + mediaPath);
        if (entry is null)
            return false;

        // ファイル保存場所: アプリケーション専用キャッシュフォルダー/動画フォルダ/moviePath (既存の同名ファイルが存在する場合は上書き保存)
        string mediaCacheDirectory = Path.GetFullPath(Path.Combine(FileSystem.Current.CacheDirectory, root));
        if (!Directory.Exists(mediaCacheDirectory))
            Directory.CreateDirectory(mediaCacheDirectory);

        string tempMedia = Path.GetFullPath(Path.Combine(mediaCacheDirectory, mediaPath));
        if (!File.Exists(tempMedia))
            entry.ExtractToFile(tempMedia, true);

        mediaElement.Source = MediaSource.FromUri(tempMedia);
        if (isMovie)
            mediaElement.IsVisible = true;
        mediaElement.Play();

        // 動画のスキップボタンを実装したら便利そう
        return true;
    }
}
