using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    public class Entry
    {
        [Key]   
        public Guid Id { get; set; }

        public DateTime DateEntered { get; set; }

        public bool IsPto { get; set; }

        public bool IsHoliday { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public string TimeZone { get; set; } = "";

        #region Relationships

        [ForeignKey("IssueStatus")]
        public int IssueStatusId { get; set; }

        public virtual IssueStatus IssueStatus { get; set; }

        [ForeignKey("CommitmentId")]
        public Guid CommitmentId { get; set; }

        public virtual Commitment Commitment { get; set; }

        #endregion
    }
}
