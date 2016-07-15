using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Team
    /// </summary>
    public class Team
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        #endregion

        #region Relationships
        public virtual ICollection<Employee> Members { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        #endregion
    } 
}