using DungAT.DockerMonitoring.Models.ViewModels;

namespace DungAT.DockerMonitoring.Application.Abstractions;

public interface IDockerService
{
    Task<List<ContainerViewModels>> GetAllContainersAsync();
}