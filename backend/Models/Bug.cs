namespace BugManagementAPI.Models
{
    public enum BugStatus
    {
        Open,
        Closed,
        WorkInProgress,
        Hold,
        Rejected
    }

    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugStatus Status { get; set; } = BugStatus.Open;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
