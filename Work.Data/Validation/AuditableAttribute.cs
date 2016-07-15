using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Work.Data.Validation
{
    /// <summary>
    /// Custom attribute to automatically set date/time and user properties
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Interface,
        AllowMultiple = false)]
    public class AuditableAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IAuditable))
                throw new InvalidOperationException(
                    "Must implement IAuditData");

            var data = value as IAuditable;

            // set audit data properties
            if (data.Id == 0)
            {
                // first save
                if (data.CreatedOn <= DateTime.MinValue)
                    data.CreatedOn = DateTime.UtcNow;

                if (string.IsNullOrWhiteSpace(data.CreatedBy))
                {
                    var username = Thread.CurrentPrincipal.Identity.Name;
                    if (string.IsNullOrWhiteSpace(username))
                        username = "system";

                    data.CreatedBy = username;
                }
            }
            else
            {
                // subsequent save
                    var username = Thread.CurrentPrincipal.Identity.Name;
                    if (string.IsNullOrWhiteSpace(username))
                        username = "system";

                    data.UpdatedOn = DateTime.UtcNow;
                    data.UpdatedBy = username;
            }
            return ValidationResult.Success;
        }
    }
}
