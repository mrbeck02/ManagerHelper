using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    public class Commitment
    {
        [Key]
        public Guid Id { get; set; }

        public bool DidComplete { get; set; }

        public bool IncludeInData { get; set; }

        public bool WasInitiallyCommitted { get; set; }

        public string Notes { get; set; } // Notes about the commitment and why it might not have been completed

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public string TimeZone { get; set; } = "";


        #region Relationships

        [ForeignKey("Sprint")]
        public Guid SprintId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [ForeignKey("JiraIssue")]
        public Guid JiraIssueId { get; set; }

        public virtual JiraIssue JiraIssue { get; set; }

        [ForeignKey("Developer")]
        public Guid DeveloperId { get; set; }

        public virtual Developer Developer { get; set; }

        #endregion
    }
}
