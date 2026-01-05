namespace AliceNovel.Controls;

internal class FileLoader
{
    /// <summary>
    /// Clears all files and directories from the application's cache directory.
    /// </summary>
    public static void ClearCache()
    {
        string path = FileSystem.Current.CacheDirectory;
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }
}
