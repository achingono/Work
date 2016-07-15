using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class Employee
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Editable(true),
        DataType(DataType.Text),
        StringLength(100)]
        public string FirstName { get; set; }

        [Editable(true),
        DataType(DataType.Text),
        StringLength(100)]
        public string LastName { get; set; }

        [Editable(true),
        DataType(DataType.EmailAddress),
        StringLength(256)]
        public string Email { get; set; }

        [Editable(true),
        DataType(DataType.Text),
        StringLength(256)]
        public string Position { get; set; }

        [Editable(true),
        DataType(DataType.Date)]
        public DateTime HiredOn { get; set; }

        [Editable(true),
        DataType(DataType.Date)]
        public DateTime? TerminatedOn { get; set; }
        #endregion

        #region Relationships
        [Editable(true),
        DataType(DataType.Text)]
        public virtual Employee Manager { get; set; }

        [XmlIgnore]
        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        #endregion
    }
}