using Hangfire.Console.Extensions;
using Serilog;
using Serilog.Events;

namespace DungAT.DockerMonitoring.WebApi.Extensions;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddHangfireConsoleExtensions();
        return builder;
    }
}