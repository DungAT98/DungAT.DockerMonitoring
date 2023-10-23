using DungAT.DockerMonitoring.WebApi.Filters;
using DungAT.DockerMonitoring.WebApi.Infrastructures;
using Hangfire;
using Hangfire.Console;

namespace DungAT.DockerMonitoring.WebApi.Extensions;

public static class HangfireExtensions
{
    public static WebApplicationBuilder UseHangfire(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseConsole()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSerilogLogProvider()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DatabaseConnection")));
        builder.Services.AddHangfireServer((serviceProvider, options) =>
        {
            options.Activator = new ServiceProviderJobActivator(serviceProvider);
        });

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
}