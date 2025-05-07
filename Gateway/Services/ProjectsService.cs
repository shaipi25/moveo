using Dto;
using FluentValidation;
using Gateway.Model.Queries;
using MassTransit;
using Requests;
using Responses;

namespace Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IRequestClient<CreateProjectRequest> _createProjectRequestClient;
        private readonly IRequestClient<GetProjectRequest> _getProjectRequestClient;
        private readonly IRequestClient<GetAllProjectsRequest> _getAllProjectsRequestClient;
        private readonly IRequestClient<UpdateProjectRequest> _updateProjectRequestClient;
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IValidator<CreateProjectRequestDto> _createProjectValidator;
        private readonly IValidator<GetAllProjectsQuery> _getAllProjectsvalidator;
        private readonly IValidator<UpdateProjectRequestDto> _updateProjectRequestValidator;

        public ProjectsService(
            IRequestClient<CreateProjectRequest> createProjectRequestClient,
            IRequestClient<GetProjectRequest> getProjectRequestClient,
            IRequestClient<GetAllProjectsRequest> getAllProjectsRequestClient,
            IRequestClient<UpdateProjectRequest> updateProjectRequestClient,
            IPublishEndpoint publishEndpoint,
            IValidator<CreateProjectRequestDto> createProjectValidator,
            IValidator<GetAllProjectsQuery> getAllProjectsvalidator,
            IValidator<UpdateProjectRequestDto> updateProjectRequestValidator)
        {
            _createProjectRequestClient = createProjectRequestClient;
            _getProjectRequestClient = getProjectRequestClient;
            _getAllProjectsRequestClient = getAllProjectsRequestClient;
            _updateProjectRequestClient = updateProjectRequestClient;
            _publishEndpoint = publishEndpoint;

            _createProjectValidator = createProjectValidator;
            _getAllProjectsvalidator = getAllProjectsvalidator;
            _updateProjectRequestValidator = updateProjectRequestValidator;
        }

        public async Task<ProjectDto> GetAsync(Guid projectId, string userName)
        {
            var request = new GetProjectRequest
            {
                Id = projectId,
                UserName = userName
            };
            var result = await _getProjectRequestClient.GetResponse<ProjectDto>(request);
           
            return result.Message;
        }

        public async Task<GetAllProjectsResponse> GetAllAsync(GetAllProjectsQuery query, string userName)
        {
            _getAllProjectsvalidator.ValidateAndThrow(query);

            var request = new GetAllProjectsRequest
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                UserName = userName
            };
            var result =  await _getAllProjectsRequestClient.GetResponse<GetAllProjectsResponse>(request);
            return result.Message;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectRequestDto request, string userName)
        {
            _createProjectValidator.ValidateAndThrow(request);

            var createProjectRequest = new CreateProjectRequest
            {
                Name = request.Name,
                Description = request.Description,
                UserName = userName
            };
            var result = await _createProjectRequestClient.GetResponse<ProjectDto>(createProjectRequest);
            return result.Message;
        }

        public async Task<ProjectDto> UpdateAsync(Guid projectId, UpdateProjectRequestDto request, string userName)
        {
            _updateProjectRequestValidator.ValidateAndThrow(request);

            var updateProjectRequest = new UpdateProjectRequest
            {
                Id = projectId,
                Name = request.Name,
                Description = request.Description,
                UserName = userName
            };
            var result = await _updateProjectRequestClient.GetResponse<ProjectDto>(updateProjectRequest);
            return result.Message;
        }

        public async Task Delete(Guid projectId, string userName)
        {
            var request = new DeleteProjectRequest
            {
                Id = projectId,
                UserName = userName
            };
            await _publishEndpoint.Publish(request);
        }

    }
}
