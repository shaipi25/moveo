using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class GetAllProjectsConsumer : IConsumer<GetAllProjectsRequest>
    {
        private readonly IProjectsRepository _repository;

        public GetAllProjectsConsumer(IProjectsRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<GetAllProjectsRequest> context)
        {
            var request = context.Message;
            var response = _repository.GetAll(request);
            return context.RespondAsync(response);
        }
    }
}
