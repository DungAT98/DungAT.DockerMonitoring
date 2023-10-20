using DungAT.DockerMonitoring.WebApi.BackgroundServices;
using DungAT.DockerMonitoring.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddSerilog()
    .UseHangfire();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .RegisterConfigurations()
    .RegisterServices();

builder.Services.AddHostedService<LifetimeEventsHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();