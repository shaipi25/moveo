using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using moveo.Services;

namespace moveo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksService _tasksService;

        public TasksController(ILogger<TasksController> logger, ITasksService tasksService)
        {
            _logger = logger;
            _tasksService = tasksService;
        }

        [HttpGet("GetTask")]
        public ActionResult Get(string projectName, string taskName)
        {
            return Ok();
        }

        [HttpPost("CreateTask")]
        public ActionResult Create(string projectName, string taskName, string status, string description)
        {
            return Ok();
        }

        [HttpPut("UpdateTask")]
        public ActionResult Update(string projectName, string taskName, string newTaskName, string status, string description)
        {
            return Ok();
        }

        [HttpPut("DeleteTask")]
        public ActionResult Delete(string projectName, string taskName)
        {
            return Ok();
        }
    }
}
