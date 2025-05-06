namespace Dto
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }

        public string UserName { get; set; }
    }
}
