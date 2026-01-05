using System.Text.Json.Serialization;

namespace AliceNovel.Controls;

internal class FileSaver
{

}

internal class SaveDataInfo
{
    [JsonPropertyName("GameTitle")]
    public string GameTitle { get; set; }

    [JsonPropertyName("GameEngine")]
    public string GameEngine { get; set; }

    [JsonPropertyName("EngineVersion")]
    public string EngineVersion { get; set; }

    [JsonPropertyName("SaveLists")]
    public IList<SaveDataLists> SaveLists { get; set; }

    public class SaveDataLists
    {
        [JsonPropertyName("CurrentLines")]
        public int CurrentLines { get; set; }

        [JsonPropertyName("LastUpdated")] // Format: ISO8601
        public string LastUpdated { get; set; }
    }
}
