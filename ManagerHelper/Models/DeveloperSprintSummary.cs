namespace ManagerHelper.Models
{
    public class DeveloperSprintSummary
    {
        public bool CountSprintValues { get; set; }
        public int CommittedStoryPoints { get; set; }
        public int CompletedStoryPoints { get; set; }
        public float CompletionPercentage { get; set; }
        public int RolloverIssueCount { get; set; }
        public int SupportIssueCount { get; set; }
    }
}
