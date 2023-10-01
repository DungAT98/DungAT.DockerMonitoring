using Docker.DotNet;
using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Models.ViewModels;

namespace DungAT.DockerMonitoring.Application.Services;

public class DockerMonitoring : IDockerMonitoring
{
    public DockerMonitoring()
    {
        
    }
    
    public Task<List<ContainerViewModels>> GetAllContainersAsync()
    {
        return null;
        // using var client = new DockerClientConfiguration(new Uri()).CreateClient();
    }
}