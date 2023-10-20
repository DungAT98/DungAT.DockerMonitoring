using DungAT.DockerMonitoring.Application.Services;
using DungAT.DockerMonitoring.Models.Configurations;
using Hangfire;
using Microsoft.Extensions.Options;

namespace DungAT.DockerMonitoring.WebApi.BackgroundServices;

public class LifetimeEventsHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IOptions<List<CloudFlareConfiguration>> _cloudFlareConfigurations;
    private const string CronExpression = "*/1 * * * *";

    public LifetimeEventsHostedService(
        ILogger<LifetimeEventsHostedService> logger,
        IHostApplicationLifetime appLifetime, IOptions<List<CloudFlareConfiguration>> cloudFlareConfigurations)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _cloudFlareConfigurations = cloudFlareConfigurations;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _logger.LogInformation("OnStarted has been called");

        foreach (var cloudFlareConfiguration in _cloudFlareConfigurations.Value)
        {
            RecurringJob.AddOrUpdate<CloudFlareDnsUpdateService>(
                $"{typeof(CloudFlareDnsUpdateService)} - {cloudFlareConfiguration.DomainNames.FirstOrDefault()}",
                n => n.UpdateAsync(cloudFlareConfiguration), CronExpression);
        }
    }

    private void OnStopping()
    {
        _logger.LogInformation("OnStopping has been called");
        foreach (var cloudFlareConfiguration in _cloudFlareConfigurations.Value)
        {
            RecurringJob.RemoveIfExists(
                $"{typeof(CloudFlareDnsUpdateService)} - {cloudFlareConfiguration.DomainNames.FirstOrDefault()}");
        }
    }

    private void OnStopped()
    {
        _logger.LogInformation("OnStopped has been called");

        // Perform post-stopped activities here
    }
}