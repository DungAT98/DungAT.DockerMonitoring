using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Application.Services;
using DungAT.DockerMonitoring.Models.Configurations;
using Serilog;

namespace DungAT.DockerMonitoring.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterConfigurations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<DockerCredential>().Configure<IConfiguration>((setting, configuration) =>
        {
            configuration.GetSection("DockerCredential").Bind(setting);
        });
        
        serviceCollection.AddOptions<List<CloudFlareConfiguration>>().Configure<IConfiguration>((setting, configuration) =>
        {
            configuration.GetSection("CloudFlareConfigurations").Bind(setting);
        });
        
        serviceCollection.AddOptions<List<DuckDnsConfiguration>>().Configure<IConfiguration>((setting, configuration) =>
        {
            configuration.GetSection("DuckDnsConfigurations").Bind(setting);
        });

        return serviceCollection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDockerService, DockerService>();
        return serviceCollection;
    }
}