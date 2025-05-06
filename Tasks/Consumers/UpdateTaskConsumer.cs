using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class UpdateTaskConsumer : IConsumer<UpdateTaskRequest>
    {
        private readonly ITasksRepository _repository;

        public UpdateTaskConsumer(ITasksRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<UpdateTaskRequest> context)
        {
            var request = context.Message;
            var response = _repository.Update(request);
            return context.RespondAsync(response);
        }
    }
}
