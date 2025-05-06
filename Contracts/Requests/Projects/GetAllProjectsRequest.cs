namespace Requests
{
    public class GetAllProjectsRequest
    {
        public string UserName { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
