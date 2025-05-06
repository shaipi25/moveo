using Dto;

namespace moveo.Requests
{
    public class UpdateTaskRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
