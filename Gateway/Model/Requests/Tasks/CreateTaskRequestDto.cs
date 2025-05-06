using Dto;

namespace Requests
{
    public class CreateTaskRequestDto
    {
        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
