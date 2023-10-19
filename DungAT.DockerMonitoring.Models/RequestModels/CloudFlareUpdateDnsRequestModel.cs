using System.Text.Json.Serialization;

namespace DungAT.DockerMonitoring.Models.RequestModels;

public class CloudFlareUpdateDnsRequestModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("proxied")]
    public bool Proxied { get; set; } = true;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("ttl")]
    public int Ttl { get; set; } = 120;
}