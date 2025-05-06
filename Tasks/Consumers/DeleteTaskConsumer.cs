using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class DeleteTaskConsumer : IConsumer<DeleteTaskRequest>
    {
        private readonly ITasksRepository _repository;

        public DeleteTaskConsumer(ITasksRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<DeleteTaskRequest> context)
        {
            var request = context.Message;
            _repository.Delete(request);
            return Task.CompletedTask;
        }
    }
}
