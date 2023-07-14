using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    public enum IssueStatusEnum
    {
        open = 1, todo, inprogress, readyfortest, intest, readyforrelease, done, unknown
    };

    /// <summary>
    /// This represents the allowed status value for each entry
    /// </summary>
    public class IssueStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = "";
    }
}
