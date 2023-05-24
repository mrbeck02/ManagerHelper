using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManagerHelper.Data.Entities
{
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
