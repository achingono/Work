using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Work.Data;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Task
    /// </summary>
    public class TaskModel
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public EstimationModel Estimated { get; set; }

        public State State { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
        
        #endregion

        #region Relationships
        public virtual ProjectModel Project { get; set; }
        #endregion
    } 
}