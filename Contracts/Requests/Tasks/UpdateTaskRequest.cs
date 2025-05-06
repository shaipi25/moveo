using Dto;

namespace Requests
{
    public class UpdateTaskRequest
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }

        public string UserName { get; set; }
    }
}
