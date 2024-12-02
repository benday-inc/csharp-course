using System.Text.Json.Serialization;

namespace LinqLab.Api;

public class Movie
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("cast")]
    public string[] Cast { get; set; } = new string[0];

    [JsonPropertyName("genres")]
    public string[] Genres { get; set; } = new string[0];

    [JsonPropertyName("href")]
    public string Href { get; set; } = string.Empty;

    [JsonPropertyName("extract")]
    public string Extract { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail_width")]
    public int ThumbnailWidth { get; set; }

    [JsonPropertyName("thumbnail_height")]
    public int ThumbnailHeight { get; set; }

    public override string ToString()
    {
        return $"{Title} ({Year})";
    }
}


