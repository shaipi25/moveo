using Excpetions;
using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class CreateProjectConsumer : IConsumer<CreateProjectRequest>
    {
        private readonly IProjectsRepository _repository;

        public CreateProjectConsumer(IProjectsRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<CreateProjectRequest> context)
        {
            var request = context.Message;

            if (_repository.DoesProjectExists(request.Name, request.UserName))
            {
                throw new ConflictException($"Project {request.Name} alread exists");
            }
            
            var response = _repository.Create(request);
            return context.RespondAsync(response);
        }
    }
}
