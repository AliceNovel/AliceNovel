using System.Text.Json.Serialization;

namespace AliceNovel.Controls;

internal class FileSaver
{

}

internal class SaveDataInfo
{
    public string GameTitle { get; set; }

    public string GameEngine { get; set; }

    public string EngineVersion { get; set; }

    public IList<SaveDataLists> SaveLists { get; set; }

    public class SaveDataLists
    {
        public int CurrentLines { get; set; }

        /// <remarks>
        /// Format: ISO8601
        /// </remarks>
        public DateTimeOffset LastUpdated { get; set; }
    }
}
