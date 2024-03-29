﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHelper.Data.Entities
{
    /// <summary>
    /// This represents a sprint which a commitment was made in.  It can be used for other 
    /// purposes too.
    /// </summary>
    public class Sprint
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        #region Relationships

        [ForeignKey("Quarter")]
        public Guid QuarterId { get; set; }

        public virtual Quarter Quarter { get; set; }

        #endregion
    }
}
