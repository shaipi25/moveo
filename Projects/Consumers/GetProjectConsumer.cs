using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class GetProjectConsumer : IConsumer<GetProjectRequest>
    {
        private readonly IProjectsRepository _repository;

        public GetProjectConsumer(IProjectsRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<GetProjectRequest> context)
        {
            var request = context.Message;
            var response = _repository.Get(request);
            return context.RespondAsync(response);
        }
    }
}
