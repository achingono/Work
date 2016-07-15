using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Contact
    /// </summary>
    [ComplexType]
    public class Contact
    {
        #region Properties
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        public Address Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [DataType(DataType.EmailAddress),
        StringLength(256)]
        public string Email { get; set; }
        #endregion
    } 
}