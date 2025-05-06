using Excpetions;
using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class DeleteProjectConsumer : IConsumer<DeleteProjectRequest>
    {
        private readonly IProjectsRepository _repository;

        public DeleteProjectConsumer(IProjectsRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<DeleteProjectRequest> context)
        {
            var request = context.Message;
            _repository.Delete(request);
            return Task.CompletedTask;
        }
    }
}
