using Dto;
using Gateway.Model.Queries;
using Requests;
using Responses;

namespace Services
{
    public interface IProjectsService
    {
        public Task<ProjectDto> GetAsync(Guid projectId, string userName);

        public Task<GetAllProjectsResponse> GetAllAsync(GetAllProjectsQuery getAllProjectsQuery, string userName);

        public Task<ProjectDto> CreateAsync(CreateProjectRequestDto request, string userName);

        public Task<ProjectDto> UpdateAsync(Guid projectId, UpdateProjectRequestDto request, string userName);

        public Task Delete(Guid projectId, string userName);
    }
}