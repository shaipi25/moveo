using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class GetTaskConsumer : IConsumer<GetTaskRequest>
    {
        private readonly ITasksRepository _repository;

        public GetTaskConsumer(ITasksRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<GetTaskRequest> context)
        {
            var request = context.Message;
            var response = _repository.Get(request);
            return context.RespondAsync(response);
        }
    }
}
