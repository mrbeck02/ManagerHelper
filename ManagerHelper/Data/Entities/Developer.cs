using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    /// <summary>
    /// This represents the developer that did the work in a commitment.  This helps with gethering
    /// developer metrics.
    /// </summary>
    public class Developer
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public string TimeZone { get; set; } = "";

        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
