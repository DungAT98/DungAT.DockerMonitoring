namespace DungAT.DockerMonitoring.Models.Abstractions;

public interface IDnsConfiguration
{
    public List<string> DomainNames { get; set; }
}