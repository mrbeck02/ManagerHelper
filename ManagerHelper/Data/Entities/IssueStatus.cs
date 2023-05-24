using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    public enum IssueStatusEnum
    {
        open = 1, todo, inprogress, readyfortest, intest, readyforrelease, done, unknown
    };

    public class IssueStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = "";
    }
}
