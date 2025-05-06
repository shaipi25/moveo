using Dto;
using Excpetions;
using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class CreateTaskConsumer : IConsumer<CreateTaskRequest>
    {
        private readonly ITasksRepository _repository;
        private readonly IRequestClient<GetProjectRequest> _getProjectRequestClient;

        public CreateTaskConsumer(ITasksRepository repository, IRequestClient<GetProjectRequest> getProjectRequestClient)
        {
            _repository = repository;
            _getProjectRequestClient = getProjectRequestClient;
        }

        public Task Consume(ConsumeContext<CreateTaskRequest> context)
        {
            var request = context.Message;

            var project = _getProjectRequestClient.GetResponse<ProjectDto>(
                new GetProjectRequest
                {
                    Id = request.ProjectId,
                    UserName = request.UserName
                });

            if (project == null)
            {
                throw new NotFoundException($"Could not find project {request.ProjectId}");
            }

            if(!_repository.DoesTaskExists(request.ProjectId, request.Name, request.UserName))
            {
                throw new NotFoundException($"Could not find task {request.Name} at project {request.ProjectId}");
            }

            var response = _repository.Create(request);
            return context.RespondAsync(response);
        }
    }
}
