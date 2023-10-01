using DungAT.DockerMonitoring.Models.ViewModels;

namespace DungAT.DockerMonitoring.Application.Abstractions;

public interface IDockerMonitoring
{
    Task<List<ContainerViewModels>> GetAllContainersAsync();
}