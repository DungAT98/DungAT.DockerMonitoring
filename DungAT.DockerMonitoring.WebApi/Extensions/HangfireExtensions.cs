using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Application.Services;
using DungAT.DockerMonitoring.Models.Configurations;
using DungAT.DockerMonitoring.WebApi.Filters;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DungAT.DockerMonitoring.WebApi.Extensions;

public static class HangfireExtensions
{
    public static WebApplicationBuilder UseHangfire(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DatabaseConnection")));
        builder.Services.AddHangfireServer();

        return builder;
    }

    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder builder)
    {
        builder.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangFireAuthorizationFilter() }
        });

        return builder;
    }

    // public static IApplicationBuilder InitBackgroundJob(this WebApplicationBuilder builder)
    // {
    //     var cronExpression = "*/10 * * * *";
    //     var serviceProvider = builder.Services.BuildServiceProvider();
    //     var cloudFlareConfigurations = serviceProvider.GetRequiredService<IOptions<List<CloudFlareConfiguration>>>();
    //     var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
    //     foreach (var cloudFlareConfiguration in cloudFlareConfigurations.Value)
    //     {
    //         var dnsUpdateService = new CloudFlareDnsUpdateService(cloudFlareConfiguration,
    //             loggerFactory.CreateLogger<CloudFlareDnsUpdateService>());
    //         RecurringJob.AddOrUpdate<CloudFlareDnsUpdateService>(cloudFlareConfiguration.ZoneId,
    //             n => n.UpdateAsync(), cronExpression);
    //     }
    // }
}