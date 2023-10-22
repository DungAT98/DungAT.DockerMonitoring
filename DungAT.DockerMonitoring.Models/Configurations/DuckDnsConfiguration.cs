using DungAT.DockerMonitoring.Models.Abstractions;

namespace DungAT.DockerMonitoring.Models.Configurations;

public class DuckDnsConfiguration : IDnsConfiguration
{
    public string Token { get; set; } = default!;

    public List<string> DomainNames { get; set; } = new();
}