using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using moveo.Services;

namespace moveo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IProjectsService _projectsService;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    {
        _logger = logger;
        _projectsService = projectsService;
    }

    [HttpGet("GetProject")]
    public ActionResult Get([FromQuery] string projectName)
    {
        return Ok();
    }

    [HttpPost("CreateProject")]
    public ActionResult CreateProject(string name, string description)
    {
        return Ok();
    }

    [HttpPut("UpdateProject")]
    public ActionResult Update([FromQuery] string projectName)
    {
        return Ok();
    }

    [HttpDelete("DeleteProject")]
    public ActionResult Delete([FromQuery] string projectName)
    {
        return Ok();
    }
}
