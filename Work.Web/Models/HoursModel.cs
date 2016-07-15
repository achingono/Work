using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Hours
    /// </summary>
    public class HoursModel
    {
        #region Properties
        public double Billable { get; set; }
        public double NonBillable { get; set; }
        public double DoubleRate { get; set; }
        public double ERS { get; set; }
        #endregion
    } 
}