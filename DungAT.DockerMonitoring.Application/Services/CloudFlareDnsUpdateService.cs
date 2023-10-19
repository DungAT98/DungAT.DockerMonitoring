using DungAT.DockerMonitoring.Application.Abstractions;
using DungAT.DockerMonitoring.Models.Configurations;
using DungAT.DockerMonitoring.Models.RequestModels;
using DungAT.DockerMonitoring.Models.ResponseModels;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace DungAT.DockerMonitoring.Application.Services;

public class CloudFlareDnsUpdateService : IDnsUpdateService
{
    private readonly CloudFlareConfiguration _cloudflareConfiguration;
    private readonly ILogger<CloudFlareDnsUpdateService> _logger;

    public CloudFlareDnsUpdateService(CloudFlareConfiguration cloudflareConfiguration, ILogger<CloudFlareDnsUpdateService> logger)
    {
        _cloudflareConfiguration = cloudflareConfiguration;
        _logger = logger;
    }

    public async Task UpdateAsync(string currentIpAddress)
    {
        var restClient = new RestClient("https://api.cloudflare.com");
        var currentDnsRecords = await GetAllCurrentDnsRecordsAsync();
        foreach (var domainName in _cloudflareConfiguration.DomainNames)
        {
            var record = currentDnsRecords.FirstOrDefault(x => x.Name == domainName);
            if (record == null)
            {
                _logger.LogWarning("Cannot find DNS record for domain {DomainName}", domainName);
                continue;
            }
            
            var request = new RestRequest("client/v4/zones/{zoneId}/dns_records/{id}", Method.Put);
            request.AddUrlSegment("zoneId", _cloudflareConfiguration.ZoneId);
            request.AddUrlSegment("id", record.Id);
            var command = new CloudFlareUpdateDnsRequestModel
            {
                Id = record.Id,
                Type = record.Type,
                Content = currentIpAddress,
                Name = record.Name,
                Proxied = record.Proxied,
                Ttl = record.Ttl
            };
            
            request.AddJsonBody(command);
            var response = await restClient.ExecutePutAsync(request);
            if (response.IsSuccessful)
            {
                _logger.LogInformation("Update DNS record for domain {DomainName} successfully", domainName);
            }
            else
            {
                _logger.LogWarning("Cannot update DNS record for domain {DomainName}", domainName);
            }
        }
    }

    public async Task UpdateAsync()
    {
        var currentIpAddress = await GetCurrentIpAddress();
        await UpdateAsync(currentIpAddress);
    }

    private async Task<List<CloudFlareDnsResponseModel>> GetAllCurrentDnsRecordsAsync()
    {
        var restClient = new RestClient("https://api.cloudflare.com");
        var request = new RestRequest("client/v4/zones/{zoneId}/dns_records");
        request.AddUrlSegment("zoneId", _cloudflareConfiguration.ZoneId);
        // maximum page size in cloudflare is 50000
        request.AddQueryParameter("per_page", 50000);
        var response = await restClient.ExecuteGetAsync<List<CloudFlareDnsResponseModel>>(request);
        if (response is { IsSuccessful: true, Content: not null, Data: not null })
        {
            return response.Data;
        }

        throw new Exception("Cannot get current DNS records");
    }

    private async Task<string> GetCurrentIpAddress()
    {
        var restClient = new RestClient("https://api.ipify.org");
        var request = new RestRequest();
        var response = await restClient.ExecuteGetAsync(request);
        if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
        {
            return response.Content;
        }

        throw new Exception("Cannot get current IP address");
    }
}