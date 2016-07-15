using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Client
    /// </summary>
    public class ClientModel
    {
        #region Properties
        public int Id { get; set; }

        [Required,
         StringLength(100, MinimumLength = 5)]
        public string Company { get; set; }

        public ContactModel Mailing { get; set; }

        public ContactModel Billing { get; set; }

        public ContactModel Technical { get; set; }

        [Range(0.0, 100.0)]
        public double Rate { get; set; }

        [MaxLength, 
         DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        #endregion
    }
}