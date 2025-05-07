using Gateway.Model.Queries;
using Microsoft.AspNetCore.Mvc;
using Requests;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpGet("projects/{projectId}")]
    public async Task<ActionResult> Get([FromRoute] Guid projectId)
    {
        var result = await _projectsService.GetAsync(projectId, this.GetUserName());
        return Ok(result);
    }


    [HttpGet("projects/getAll")]
    public async Task<ActionResult> GetAll([FromQuery] GetAllProjectsQuery getAllProjectsQuery)
    {
       
        var result = await _projectsService.GetAllAsync(getAllProjectsQuery, this.GetUserName());
        return Ok(result);
    }

    [HttpPost("projects/create")]
    public async Task<ActionResult> Create([FromBody] CreateProjectRequestDto request)
    {
        var result = await _projectsService.CreateAsync(request, this.GetUserName());
        return Ok(result);
    }

    [HttpPut("projects/{projectId}/update")]
    public async Task<ActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequestDto request)
    {
        var result = await _projectsService.UpdateAsync(projectId, request, this.GetUserName());
        return Ok(result);
    }

    [HttpDelete("projects/{projectId}")]
    public async Task<ActionResult> Delete([FromRoute] Guid projectId)
    {
        await _projectsService.Delete(projectId, this.GetUserName());
        return Ok();
    }


}
