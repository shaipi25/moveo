using Dto;
using Requests;

namespace Repositories
{
    public interface IProjectsRepository
    {
        public ProjectDto Get(GetProjectRequest request);

        public List<ProjectDto> GetAll(GetAllProjectsRequest request);

        public ProjectDto Create(CreateProjectRequest request);

        public ProjectDto Update(UpdateProjectRequest request);

        public void Delete(DeleteProjectRequest request);
    }
}
