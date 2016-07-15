using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Web
{
    /// <summary>
    /// Summary description for Estimation
    /// </summary>
    public class EstimationModel
    {
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Hours { get; set; }
    }
}