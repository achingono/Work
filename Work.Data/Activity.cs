using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Activity
    /// </summary>
    public class Activity: IAuditable
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        public Hours Hours { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [MaxLength]
        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
        #endregion

        #region Relationships
        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
        #endregion
    } 
}