using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Activity
    /// </summary>
    public class ActivityModel
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        public HoursModel Hours { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [MaxLength]
        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
        #endregion
    } 
}