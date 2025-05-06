namespace Requests
{
    public class GetAllTasksRequest
    {
        public Guid ProjectId { get; set; }

        public string UserName { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
