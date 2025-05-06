using Dto;
using Requests;

namespace Repositories
{
    public interface ITasksRepository
    {
        public TaskDto Create(CreateTaskRequest request);

        public TaskDto Get(GetTaskRequest request);

        public List<TaskDto> GetAll(GetAllTasksRequest request);

        public TaskDto Update(UpdateTaskRequest request);

        public void Delete(DeleteTaskRequest request);
    }
}
