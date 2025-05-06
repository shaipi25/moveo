using Dto;
using Requests;

namespace Services
{
    public interface ITasksService
    {
        public Task<TaskDto> Get(Guid projectId, Guid taskId, string userName);

        public Task<List<TaskDto>> GetAll(Guid projectId, int? pageNumber, int? pageSize, string userName);

        public Task<TaskDto> Create(CreateTaskRequestDto request, string userName);

        public Task<TaskDto> Update(Guid projectId, Guid taskId, UpdateTaskRequestDto request, string userName);

        public void Delete(Guid projectId, Guid taskId, string userName);
    }
}
