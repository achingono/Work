using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Project
    /// </summary>
    public class ProjectModel
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        #endregion

        #region Relationships
        public virtual TeamModel Team { get; set; }

        public virtual ClientModel Client { get; set; }
        #endregion
    }
}