using Dto;
using Requests;

namespace Services
{
    public interface IProjectsService
    {
        public Task<ProjectDto> GetAsync(Guid projectId, string userName);

        public Task<List<ProjectDto>> GetAllAsync(int? pageNumber, int? pageSize, string userName);

        public Task<ProjectDto> CreateAsync(CreateProjectRequestDto request, string userName);

        public Task<ProjectDto> UpdateAsync(Guid projectId, UpdateProjectRequestDto request, string userName);

        public Task Delete(Guid projectId, string userName);
    }
}