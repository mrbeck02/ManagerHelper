using System.ComponentModel.DataAnnotations;

namespace ManagerHelper.Data.Entities
{
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
