namespace DungAT.DockerMonitoring.Models.Configurations;

public class CloudFlareConfiguration
{
    public string ZoneId { get; set; } = default!;

    public string ApiKey { get; set; } = default!;

    public List<string> DomainNames { get; set; } = new();
}