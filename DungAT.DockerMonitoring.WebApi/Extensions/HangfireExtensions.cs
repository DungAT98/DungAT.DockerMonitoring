using DungAT.DockerMonitoring.WebApi.Filters;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Data.SqlClient;

namespace DungAT.DockerMonitoring.WebApi.Extensions;

public static class HangfireExtensions
{
    public static WebApplicationBuilder UseHangfire(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DatabaseConnection"), new SqlServerStorageOptions
                {
                    
                }));
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
}