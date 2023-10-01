namespace DungAT.DockerMonitoring.Models.Configurations;

public class DockerConfiguration
{
    public const string SectionName = "DockerConfiguration";

    public string IpAddress { get; set; } = default!;
}