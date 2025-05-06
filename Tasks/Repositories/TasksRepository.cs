using Dto;
using Excpetions;
using Requests;

namespace Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _context;

        public TasksRepository(AppDbContext context)
        {
            _context = context;
        }

        public TaskDto Create(CreateTaskRequest request)
        {
            var task = new TaskDto
            {
                Id = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                UserName = request.UserName
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        public TaskDto Get(GetTaskRequest request)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.ProjectId == request.ProjectId &&
            t.Id == request.Id && t.UserName == request.UserName);

            if (task == null) throw new NotFoundException($"Task '{request.Id}' not found in project '{request.ProjectId}'");

            return task;
        }

        public List<TaskDto> GetAll(GetAllTasksRequest request)
        {
            var tasks = request.PageNumber.HasValue && request.PageSize.HasValue ?
                _context.Tasks
                    .Where(p => p.UserName == request.UserName)
                    .OrderBy(p => p.Name)
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToList()
                :
                _context.Tasks
                    .Where(p => p.UserName == request.UserName)
                    .OrderBy(p => p.Name)
                    .ToList();

            return tasks;
        }

        public TaskDto Update(UpdateTaskRequest request)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.ProjectId == request.ProjectId &&
            t.Id == request.Id && t.UserName == request.UserName);
            
            if (task == null) throw new NotFoundException($"Task '{request.Id}' not found in project '{request.ProjectId}'");

            task.Description = string.IsNullOrEmpty(request.Description) ? task.Description : request.Description;
            task.Status = request.Status;
            task.Name = string.IsNullOrEmpty(request.Name) ? task.Name : request.Name;

            _context.SaveChanges();

            return task;
        }

        public void Delete(DeleteTaskRequest request)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.ProjectId == request.ProjectId &&
            t.Id == request.Id && t.UserName == request.UserName);

            if (task == null) return;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public bool DoesTaskExists(Guid projectId, string taskName, string userName)
        {
            return _context.Tasks.Any(t => t.ProjectId == projectId && t.Name == taskName && t.UserName == userName);
        }
    }
}
