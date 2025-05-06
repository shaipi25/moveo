namespace Requests
{
    public class DeleteTaskRequest
    {
        public Guid ProjectId { get; set; }

        public Guid Id { get; set; }

        public string UserName {  get; set; }
    }
}
