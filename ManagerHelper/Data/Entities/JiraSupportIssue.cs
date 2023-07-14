using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    /// <summary>
    /// This will represent a jira support issue that was completed by a user.
    /// TODO: Remove this and add a Jira issue type so we can see if it's a support issue or whatever.
    /// </summary>
    public class JiraSupportIssue
    {
        [Key]
        public Guid Id { get; set; }

        public bool WasInitiallyCommitted { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public string TimeZone { get; set; } = "";


        #region Relationships

        [ForeignKey("Sprint")]
        public Guid SprintId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [ForeignKey("JiraIssueId")]
        public Guid JiraIssueId { get; set; }

        public virtual JiraIssue JiraIssue { get; set; }

        [ForeignKey("DeveloperId")]
        public Guid DeveloperId { get; set; }

        public virtual Developer Developer { get; set; }

        #endregion

    }
}
