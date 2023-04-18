using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerHelper.Data.Entities
{
    public class JiraProject
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Domain { get; set; } = "";

        #region Relationships

        public virtual ICollection<JiraIssue> JiraIssues { get; set; }

        #endregion

        public JiraProject() 
        { 
            JiraIssues = new HashSet<JiraIssue>();
        }
    }
}
