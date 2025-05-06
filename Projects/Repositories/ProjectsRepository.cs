using Dto;
using Excpetions;
using Requests;

namespace Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly AppDbContext _context;

        public ProjectsRepository(AppDbContext context)
        {
            _context = context;
        }

        public ProjectDto Create(CreateProjectRequest request)
        {
            var project = new ProjectDto
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserName = request.UserName
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        public ProjectDto Get(GetProjectRequest request)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == request.Id && p.UserName == request.UserName);
            if (project == null)
                throw new NotFoundException($"Project '{request.Id}' not found");

            return project;
        }

        public List<ProjectDto> GetAll(GetAllProjectsRequest request)
        {
            var projects = request.PageNumber.HasValue && request.PageSize.HasValue ? 
                            _context.Projects
                                .Where(p => p.UserName == request.UserName)
                                .OrderBy(p => p.Name)
                                .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                                .Take(request.PageSize.Value)
                                .ToList()
                            :
                            _context.Projects
                                .Where(p => p.UserName == request.UserName)
                                .OrderBy(p => p.Name)
                                .ToList();

            return projects;
        }

        public void Delete(DeleteProjectRequest request)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == request.Id && p.UserName == request.UserName);

            if (project == null)
                return;

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public ProjectDto Update(UpdateProjectRequest request)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == request.Id && p.UserName == request.UserName);
            if (project == null)
                throw new NotFoundException($"Project '{request.Id}' not found");

            project.Name = string.IsNullOrEmpty(request.Name) ? project.Name : request.Name;
            project.Description = string.IsNullOrEmpty(request.Description) ? project.Description : request.Description; ;
            _context.SaveChanges();

            return project;
        }

        public bool DoesProjectExists(string projectName, string userName)
        {
            return _context.Projects.Any(p => p.Name == projectName && p.UserName == userName);
        }
    }
}
