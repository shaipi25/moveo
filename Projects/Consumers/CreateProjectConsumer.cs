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
            var response = _repository.Create(request);
            return context.RespondAsync(response);
        }
    }
}
