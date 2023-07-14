using System.ComponentModel.DataAnnotations;

namespace ManagerHelper.Data.Entities
{
    /// <summary>
    /// This represents a jira project such as Orange, Purple, DataScience.  It also helps form the
    /// URL to get to the issue in a browser.
    /// </summary>
    public class JiraProject
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Domain { get; set; } = "";

        public JiraProject() 
        { 
        }
    }
}
