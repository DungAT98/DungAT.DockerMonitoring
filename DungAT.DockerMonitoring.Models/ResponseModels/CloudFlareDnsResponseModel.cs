using System.Text.Json.Serialization;

namespace DungAT.DockerMonitoring.Models.ResponseModels;

public class CloudFlareDnsResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("zone_id")]
    public string ZoneId { get; set; } = default!;

    [JsonPropertyName("zone_name")]
    public string ZoneName { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("proxied")]
    public bool Proxied { get; set; } = true;

    [JsonPropertyName("ttl")]
    public int Ttl { get; set; } = 1;
}