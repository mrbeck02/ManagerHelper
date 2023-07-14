using System.ComponentModel.DataAnnotations;

namespace ManagerHelper.Data.Entities
{
    /// <summary>
    /// This represents a quarter in the year.  It helps determine the quarterly statistics.
    /// </summary>
    public class Quarter
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public int QuarterNumber { get; set; }

        public int Year { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        #region Relationships

        public virtual ICollection<Sprint> Sprints { get; set; }

        #endregion

        public Quarter()
        {
            Sprints = new HashSet<Sprint>();
        }
    }
}
