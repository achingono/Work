using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Data
{
    public enum State
    {
        New,
        InProgress,
        Completed,
        IssuesFound,
        Deployed,
        Closed
    }
    /// <summary>
    /// Summary description for Task
    /// </summary>
    public class Task: IAuditable
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public Estimation Estimated { get; set; }

        public State State { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
        
        #endregion

        #region Relationships
        public virtual Project Project { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        #endregion
    } 
}