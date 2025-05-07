using MassTransit;
using Repositories;
using Requests;
using Responses;

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
            var projects = _repository.GetAll(request);

            var response = new GetAllProjectsResponse
            {
                Projects = projects,
            };
            return context.RespondAsync(response);
        }
    }
}
