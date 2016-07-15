using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Summary description for Estimation
/// </summary>
[ComplexType]
public class Estimation
{
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public double Hours { get; set; }
}