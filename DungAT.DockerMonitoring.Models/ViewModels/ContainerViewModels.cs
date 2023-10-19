namespace DungAT.DockerMonitoring.Models.ViewModels;

public class ContainerViewModels
{
    public string Id { get; set; } = default!;

    public string Image { get; set; } = default!;

    public string Status { get; set; } = default!;

    public string State { get; set; } = default!;

    public string ContainerName { get; set; } = default!;
}