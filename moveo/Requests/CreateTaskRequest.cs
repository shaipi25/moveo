using Dto;

namespace moveo.Requests
{
    public class CreateTaskRequest
    {
        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
