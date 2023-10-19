using Docker.DotNet;
using Docker.DotNet.Models;
using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Models.Configurations;
using DungAT.DockerMonitoring.Models.ViewModels;
using Microsoft.Extensions.Options;

namespace DungAT.DockerMonitoring.Application.Services;

public class DockerService : IDockerService
{
    private readonly DockerCredential _dockerCredential;

    public DockerService(IOptions<DockerCredential> dockerCredential)
    {
        _dockerCredential = dockerCredential.Value;
    }

    public async Task<List<ContainerViewModels>> GetAllContainersAsync()
    {
        if (string.IsNullOrWhiteSpace(_dockerCredential.EndpointUri))
        {
            throw new Exception("EndpointUri is empty");
        }

        using var client = new DockerClientConfiguration(new Uri(_dockerCredential.EndpointUri)).CreateClient();
        var result = await client.Containers.ListContainersAsync(new ContainersListParameters()
        {
            All = true
        });

        return result.Select(n => n.Names.Select(m => new ContainerViewModels
            {
                Id = n.ID,
                Image = n.Image,
                Status = n.Status,
                State = n.State,
                ContainerName = m
            }))
            .SelectMany(n => n)
            .ToList();
    }
}