namespace Requests
{
    public class GetTaskRequest
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string UserName { get; set; }
    }
}
