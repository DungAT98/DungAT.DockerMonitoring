using DungAT.DockerMonitoring.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DungAT.DockerMonitoring.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IDockerService _dockerService;

    public TestController(IDockerService dockerService)
    {
        _dockerService = dockerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _dockerService.GetAllContainersAsync();
        return Ok(result);
    }
}