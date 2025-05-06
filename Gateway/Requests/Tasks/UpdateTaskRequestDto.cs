using Dto;

namespace Requests
{
    public class UpdateTaskRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
