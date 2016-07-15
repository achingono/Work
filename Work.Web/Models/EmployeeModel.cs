using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class EmployeeModel
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public DateTime HiredOn { get; set; }

        public DateTime? TerminatedOn { get; set; }
        #endregion

        #region Relationships
        public virtual EmployeeModel Manager { get; set; }
        #endregion
    }
}