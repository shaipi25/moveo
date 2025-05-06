using Dto;
using MassTransit;
using Repositories;
using Requests;

namespace Consumers
{
    public class UpdateProjectConsumer : IConsumer<UpdateProjectRequest>
    {
        private readonly IProjectsRepository _repository;

        public UpdateProjectConsumer(IProjectsRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<UpdateProjectRequest> context)
        {
            var request = context.Message;
            var response = _repository.Update(request);
            return context.RespondAsync(response);
        }
    }
}
