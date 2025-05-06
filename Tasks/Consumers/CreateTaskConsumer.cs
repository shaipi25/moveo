using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class CreateTaskConsumer : IConsumer<CreateTaskRequest>
    {
        private readonly ITasksRepository _repository;

        public CreateTaskConsumer(ITasksRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<CreateTaskRequest> context)
        {
            var request = context.Message;
            var response = _repository.Create(request);
            return context.RespondAsync(response);
        }
    }
}
