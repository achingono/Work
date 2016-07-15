using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Data
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    [ComplexType]
    public class Address
    {
        [StringLength(255)]
        public string Street { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }
    } 
}