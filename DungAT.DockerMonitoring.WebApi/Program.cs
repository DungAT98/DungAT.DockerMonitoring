using DungAT.DockerMonitoring.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .UseHangfire();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHangfireDashboard();
app.Run();