using DungAT.DockerMonitoring.Models.Abstractions;
using DungAT.DockerMonitoring.Models.Configurations;

namespace DungAT.DockerMonitoring.Application.Abstractions;

public interface IDnsUpdateService
{
    public Task UpdateAsync(IDnsConfiguration dnsConfiguration, string currentIpAddress);

    public Task UpdateAsync(IDnsConfiguration dnsConfiguration);
}