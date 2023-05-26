using ManagerHelper.Data.Entities;

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

        public List<JiraIssue> SprintIssues { get; set; }

        public DeveloperSprintSummary(List<Commitment> commitments)
        {
            if (commitments == null || commitments.Count == 0)
                return;

            CommittedStoryPoints = commitments.Sum(c => c.IncludeInData ? c.JiraIssue.StoryPoints : 0);
            CompletedStoryPoints = commitments.Sum(c => c.IncludeInData && c.DidComplete ? c.JiraIssue.StoryPoints : 0);
            CompletionPercentage = 100 * (CommittedStoryPoints / CompletedStoryPoints);
            RolloverIssueCount = commitments.Sum(c => c.IncludeInData && !c.DidComplete ? 1 : 0);
            CountSprintValues = commitments.TrueForAll(c => c.IncludeInData);
        }
    }
}
