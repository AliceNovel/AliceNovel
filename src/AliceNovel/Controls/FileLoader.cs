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

    /// <summary>
    /// Resets the application's main page to the default shell layout if CSS is changed.
    /// </summary>
    /// <remarks>This method updates the main page of the application to a new instance of <see
    /// cref="AppShell"/> when <paramref name="useCss"/> is <see langword="true"/>. The update is performed on the UI
    /// thread using the dispatcher to ensure thread safety.</remarks>
    /// <param name="useCss">A boolean value indicating whether CSS is changed. If <see langword="false"/>, the method performs no action.</param>
    public static void CssReset(bool useCss)
    {
        if (!useCss)
            return;

        Application.Current.Windows[0].Page.Dispatcher.Dispatch(() =>
        {
            Application.Current.Windows[0].Page = new AppShell();
        });
    }
}

[Obsolete("""    
Processing related to zip archives is scheduled for discontinuation.
See also: #168 (https://github.com/AliceNovel/AliceNovel/issues/168)
""")]
internal class ZipArchiveLoader
{

}
