using Microsoft.AspNetCore.Mvc;
using Requests;
using Services;

namespace moveo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpGet("GetProject")]
    public async Task<ActionResult> Get([FromRoute] Guid projectId)
    {
        var result = await _projectsService.GetAsync(projectId, GetUserName());
        return Ok(result);
    }


    [HttpGet("GetAllProject")]
    public async Task<ActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
       
        var result = await _projectsService.GetAllAsync(pageNumber, pageSize, GetUserName());
        return Ok(result);
    }

    [HttpPost("CreateProject")]
    public async Task<ActionResult> Create([FromBody] CreateProjectRequestDto request)
    {
        var result = await _projectsService.CreateAsync(request, GetUserName());
        return Ok(result);
    }

    [HttpPut("UpdateProject")]
    public async Task<ActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequestDto request)
    {
        var result = await _projectsService.UpdateAsync(projectId, request, GetUserName());
        return Ok(result);
    }

    [HttpDelete("DeleteProject")]
    public async Task<ActionResult> Delete([FromRoute] Guid projectId)
    {
        await _projectsService.Delete(projectId, GetUserName());
        return Ok();
    }

    private string GetUserName()
    {
        var cognitoUsername = HttpContext.User.FindFirst("cognito:username")?.Value;
        return cognitoUsername;
    }
}
