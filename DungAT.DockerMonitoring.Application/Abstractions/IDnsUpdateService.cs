namespace DungAT.DockerMonitoring.Application.Abstractions;

public interface IDnsUpdateService
{
    public Task UpdateAsync(string currentIpAddress);
    
    public Task UpdateAsync();
}