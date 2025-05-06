namespace moveo.Requests
{
    public class DeleteTaskRequest
    {
        public Guid ProjectId { get; set; }

        public string TaskName { get; set; }

        public string UserName {  get; set; }
    }
}
