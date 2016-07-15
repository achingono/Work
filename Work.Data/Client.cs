using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Client
    /// </summary>
    public class Client
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Company { get; set; }

        public Contact Mailing { get; set; }

        public Contact Billing { get; set; }

        public Contact Technical { get; set; }

        [Range(0.0, 100.0)]
        public double Rate { get; set; }

        [MaxLength]
        public string Notes { get; set; }
        #endregion

        #region Relationships
        public virtual ICollection<Project> Projects { get; set; }
        #endregion
    }
}