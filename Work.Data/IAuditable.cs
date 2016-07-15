using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Work.Data.Validation;

/// <summary>
/// Summary description for IAuditable
/// </summary>
[Auditable]
interface IAuditable
{
    #region Properties
    [Key]
    int Id { get; set; }

    DateTime CreatedOn { get; set; }
    
    [StringLength(50)]
    string CreatedBy { get; set; }

    DateTime? UpdatedOn { get; set; }

    [StringLength(50)]
    string UpdatedBy { get; set; }
        
    #endregion
}