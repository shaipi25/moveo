using MassTransit;
using Repositories;
using Requests;
using Responses;

namespace Consumers
{
    class GetAllTasksConsumer : IConsumer<GetAllTasksRequest>
    {

        private readonly ITasksRepository _repository;

        public GetAllTasksConsumer(ITasksRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<GetAllTasksRequest> context)
        {
            var request = context.Message;
            var tasks = _repository.GetAll(request);

            var response = new GetAllTasksResponse
            {
                Tasks = tasks,
            };
            return context.RespondAsync(response);
        }
    }
}
