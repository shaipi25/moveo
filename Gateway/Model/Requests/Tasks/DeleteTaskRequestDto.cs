namespace Requests
{
    public class DeleteTaskRequestDto
    {
        public Guid ProjectId { get; set; }

        public string TaskName { get; set; }

        public string UserName { get; set; }
    }
}
