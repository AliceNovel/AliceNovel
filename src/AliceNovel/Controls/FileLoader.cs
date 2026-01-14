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
        if (!useCss || Application.Current.Windows.Count <= 0)
            return;

        Application.Current.Windows[0].Page.Dispatcher.Dispatch(() =>
        {
            Application.Current.Windows[0].Page = new AppShell();
        });
    }
}

internal class AnprojFormat
{
    public string RootImage { get; set; } = "image/";

    public string RootBackground { get; set; } = "image/background/";

    public string RootStory { get; set; } = "story/";

    public string RootData { get; set; } = "data/";

    public string RootAudio { get; set; } = "audio/";

    public string RootMovie { get; set; } = "movie/";

    public string RootCharacter { get; set; } = "character.json";

    public string RootSave { get; set; } = "save/";

    public string Style { get; set; } = "style.css";

    public string FirstRead { get; set; } = "main.anov";

    public string GameName { get; set; } = "";
}

//[Obsolete("Future idea")]
//internal class AnprojFormatFuture
//{
//    public string GameTitle { get; set; } = "";

//    public RootData Roots { get; set; } = new();

//    internal class RootData
//    {
//        public string InitFile { get; set; } = "main.anov";

//        public string Style { get; set; } = "style.css";

//        //public string Character { get; set; } = "character.json";

//        public string RootImage { get; set; } = "images/";

//        public string Background { get; set; } = "images/background/";

//        //public string Story { get; set; } = "stories/";

//        //public string Data { get; set; } = "data/";

//        public string Audio { get; set; } = "sounds/";

//        public string Movie { get; set; } = "movies/";

//        public string Save { get; set; } = "saves/";
//    }
//}

[Obsolete("""
Processing related to zip archives is scheduled for discontinuation.
See also: #168 (https://github.com/AliceNovel/AliceNovel/issues/168)
""")]
internal class ZipArchiveLoader
{

}
