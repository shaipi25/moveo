namespace Requests
{
    public class UpdateProjectRequest
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
