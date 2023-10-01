using Docker.DotNet;
using Docker.DotNet.Models;

namespace DungAT.DockerMonitoring.Application;

public class Class1
{
    public async Task Data()
    {
        var client = new DockerClientConfiguration(
                new Uri(""))
            .CreateClient();

        var listContainer = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                All = true
            });
    }
}