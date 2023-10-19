using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Application.Services;
using DungAT.DockerMonitoring.Models.Configurations;

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
            configuration.GetSection("CloudFlareConfiguration").Bind(setting);
        });

        return serviceCollection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDockerService, DockerService>();
        serviceCollection.AddScoped<IDnsUpdateService, CloudFlareDnsUpdateService>();
        return serviceCollection;
    }
}