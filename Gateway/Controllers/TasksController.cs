using Microsoft.AspNetCore.Mvc;
using Requests;
using Services;

namespace moveo.Controllers
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

        [HttpGet("GetTask")]
        public ActionResult Get([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            var task = _tasksService.Get(projectId, taskId, GetUserName());
            return Ok(task);
        }
        
        [HttpGet("GetAllTask")]
        public ActionResult GetAll([FromRoute] Guid projectId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var task = _tasksService.GetAll(projectId, pageNumber, pageSize, GetUserName());
            return Ok(task);
        }

        [HttpPost("CreateTask")]
        public ActionResult Create([FromBody] CreateTaskRequestDto request)
        {
            var task = _tasksService.Create(request, GetUserName());
            return Ok(task);
        }

        [HttpPut("UpdateTask")]
        public ActionResult Update([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] UpdateTaskRequestDto request)
        {
            var task = _tasksService.Update(projectId, taskId, request, GetUserName());
            return Ok(task);
        }

        [HttpPut("DeleteTask")]
        public ActionResult Delete([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            _tasksService.Delete(projectId, taskId, GetUserName());
            return Ok();
        }

        private string GetUserName()
        {
            var cognitoUsername = HttpContext.User.FindFirst("cognito:username")?.Value;
            return cognitoUsername;
        }
    }
}
