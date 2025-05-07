using Dto;
using FluentValidation;
using Gateway.Model.Queries;
using MassTransit;
using Requests;
using Responses;

namespace Services
{
    public class TasksService : ITasksService
    {
        private readonly IRequestClient<CreateTaskRequest> _createTaskRequestClient;
        private readonly IRequestClient<GetTaskRequest> _getTaskRequestClient;
        private readonly IRequestClient<GetAllTasksRequest> _getAllTasksRequestClient;
        private readonly IRequestClient<UpdateTaskRequest> _updateTaskRequestClient;
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IValidator<CreateTaskRequestDto> _createTaskValidator;
        private readonly IValidator<GetAllTasksQuery> _getAllTasksvalidator;
        private readonly IValidator<UpdateTaskRequestDto> _updateTaskRequestValidator;

        public TasksService(
            IRequestClient<CreateTaskRequest> createTaskRequestClient,
            IRequestClient<GetTaskRequest> getTaskRequestClient,
            IRequestClient<GetAllTasksRequest> getAllTasksRequestClient,
            IRequestClient<UpdateTaskRequest> updateTaskRequestClient,
            IPublishEndpoint publishEndpoint,
            IValidator<CreateTaskRequestDto> createTaskValidator,
            IValidator<GetAllTasksQuery> getAllTasksvalidator,
            IValidator<UpdateTaskRequestDto> updateTaskRequestValidator)
        {
            _createTaskRequestClient = createTaskRequestClient;
            _getTaskRequestClient = getTaskRequestClient;
            _getAllTasksRequestClient = getAllTasksRequestClient;
            _updateTaskRequestClient = updateTaskRequestClient;
            _publishEndpoint = publishEndpoint;

            _createTaskValidator = createTaskValidator;
            _getAllTasksvalidator = getAllTasksvalidator;
            _updateTaskRequestValidator = updateTaskRequestValidator;
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
        
        public async Task<GetAllTasksResponse> GetAll(Guid projectId, GetAllTasksQuery query, string userName)
        {
            _getAllTasksvalidator.ValidateAndThrow(query);

            var request = new GetAllTasksRequest
            {
                ProjectId = projectId,
                UserName = userName,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
            var result = await _getAllTasksRequestClient.GetResponse<GetAllTasksResponse>(request);
            return result.Message;
        }

        public async Task<TaskDto> Create(CreateTaskRequestDto request, string userName)
        {
            _createTaskValidator.ValidateAndThrow(request);

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
            _updateTaskRequestValidator.ValidateAndThrow(request);

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
