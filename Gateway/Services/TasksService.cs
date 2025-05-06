using Dto;
using MassTransit;
using Requests;

namespace Services
{
    public class TasksService : ITasksService
    {
        private readonly IRequestClient<CreateTaskRequestDto> _createTaskRequestClient;
        private readonly IRequestClient<GetTaskRequest> _getTaskRequestClient;
        private readonly IRequestClient<GetAllTasksRequest> _getAllTasksRequestClient;
        private readonly IRequestClient<UpdateTaskRequestDto> _updateTaskRequestClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public TasksService(
            IRequestClient<CreateTaskRequestDto> createTaskRequestClient,
            IRequestClient<GetTaskRequest> getTaskRequestClient,
            IRequestClient<GetAllTasksRequest> getAllTasksRequestClient,
            IRequestClient<UpdateTaskRequestDto> updateTaskRequestClient,
            IPublishEndpoint publishEndpoint)
        {
            _createTaskRequestClient = createTaskRequestClient;
            _getTaskRequestClient = getTaskRequestClient;
            _getAllTasksRequestClient = getAllTasksRequestClient;
            _updateTaskRequestClient = updateTaskRequestClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TaskDto> Get(Guid projectId, Guid taskId, string userName)
        {
            var request = new GetTaskRequest
            {
                Id = taskId,
                ProjectId = projectId,
                UserName = userName
            };
            var result = await _getTaskRequestClient.GetResponse<TaskDto>(request);
            return result.Message;
        }
        
        public async Task<List<TaskDto>> GetAll(Guid projectId, int? pageNumber, int? pageSize, string userName)
        {
            var request = new GetAllTasksRequest
            {
                ProjectId = projectId,
                UserName = userName,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _getAllTasksRequestClient.GetResponse<List<TaskDto>>(request);
            return result.Message;
        }

        public async Task<TaskDto> Create(CreateTaskRequestDto request, string userName)
        {
            var createTaskRequest = new CreateTaskRequest
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                ProjectId = request.ProjectId,
                UserName = userName
            };
            var result = await _createTaskRequestClient.GetResponse<TaskDto>(createTaskRequest);
            return result.Message;
        }

        public async Task<TaskDto> Update(Guid projectId, Guid taskId, UpdateTaskRequestDto request, string userName)
        {
            var updateTaskRequest = new UpdateTaskRequest
            {
                ProjectId = projectId,
                Id = taskId,
                Name = request.Name,
                Status = request.Status,
                Description = request.Description,
                UserName = userName
            };
            var result = await _updateTaskRequestClient.GetResponse<TaskDto>(updateTaskRequest);
            return result.Message;
        }

        public async void Delete(Guid projectId, Guid taskId, string userName)
        {
            var request = new DeleteTaskRequest
            {
                Id = taskId,
                ProjectId = projectId,
                UserName = userName
            };
            await _publishEndpoint.Publish(request);
        }

    }
}
