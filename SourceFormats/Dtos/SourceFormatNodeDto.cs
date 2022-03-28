namespace EncyclopediaGalactica.SourceFormats.Dtos;

using System.Text.Json.Serialization;

public class SourceFormatNodeDto
{
    public SourceFormatNodeDto()
    {
    }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}