namespace DungAT.DockerMonitoring.Models.Abstractions;

public class BaseCloudFlareResponseModel<T>
{
    public List<T> Result { get; set; } = new();

    public bool Success { get; set; }
}