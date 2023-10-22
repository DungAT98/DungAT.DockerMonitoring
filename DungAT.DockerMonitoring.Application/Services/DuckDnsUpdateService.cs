using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Models.Abstractions;
using DungAT.DockerMonitoring.Models.Configurations;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace DungAT.DockerMonitoring.Application.Services;

public class DuckDnsUpdateService : IDnsUpdateService
{
    private readonly ILogger<DuckDnsUpdateService> _logger;

    public DuckDnsUpdateService(ILogger<DuckDnsUpdateService> logger)
    {
        _logger = logger;
    }

    public async Task UpdateAsync(IDnsConfiguration dnsConfiguration, string currentIpAddress)
    {
        var restClient = new RestClient("https://www.duckdns.org");
        var request = new RestRequest("update");
        request.AddParameter("domains", string.Join(",", dnsConfiguration.DomainNames));
        request.AddParameter("token", (dnsConfiguration as DuckDnsConfiguration)!.Token);
        request.AddParameter("ip", currentIpAddress);
        var response = await restClient.ExecuteGetAsync(request);
        if (response.IsSuccessful)
        {
            _logger.LogInformation("Update DNS successfully");
        }
        else
        {
            _logger.LogError("Update DNS failed");
        }
    }

    public async Task UpdateAsync(IDnsConfiguration dnsConfiguration)
    {
        var restClient = new RestClient("https://www.duckdns.org");
        var request = new RestRequest("update");
        request.AddParameter("domains", string.Join(",", dnsConfiguration.DomainNames));
        request.AddParameter("token", (dnsConfiguration as DuckDnsConfiguration)!.Token);
        var response = await restClient.ExecuteGetAsync(request);
        if (response.IsSuccessful)
        {
            _logger.LogInformation("Update DNS successfully");
        }
        else
        {
            _logger.LogError("Update DNS failed");
        }
    }
}