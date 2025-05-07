using Gateway.Model.Queries;
using Microsoft.AspNetCore.Mvc;
using Requests;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet("projects/{projectId}/tasks/{taskId}")]
        public ActionResult Get([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            var task = _tasksService.Get(projectId, taskId, this.GetUserName());
            return Ok(task);
        }
        
        [HttpGet("projects/tasks/getAll")]
        public ActionResult GetAll([FromRoute] Guid projectId, [FromQuery] GetAllTasksQuery query)
        {
            var task = _tasksService.GetAll(projectId, query, this.GetUserName());
            return Ok(task);
        }

        [HttpPost("projects/tasks/create")]
        public ActionResult Create([FromBody] CreateTaskRequestDto request)
        {
            var task = _tasksService.Create(request, this.GetUserName());
            return Ok(task);
        }

        [HttpPut("projects/{projectId}/tasks/{taskId}/update")]
        public ActionResult Update([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] UpdateTaskRequestDto request)
        {
            var task = _tasksService.Update(projectId, taskId, request, this.GetUserName());
            return Ok(task);
        }

        [HttpPut("projects/{projectId}/tasks/{taskId}/delete")]
        public ActionResult Delete([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            _tasksService.Delete(projectId, taskId, this.GetUserName());
            return Ok();
        }
    }
}
