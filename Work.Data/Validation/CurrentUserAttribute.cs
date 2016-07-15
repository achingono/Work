using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Work.Data.Validation
{
    /// <summary>
    /// Custom attribute to set value to current user
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CurrentUserAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null
                || string.IsNullOrWhiteSpace(value.ToString()))
            {
                var instance = validationContext.ObjectInstance;
                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);

                property.SetValue(instance, Thread.CurrentPrincipal.Identity.Name, null);
            }
            return ValidationResult.Success;
        }
    }
}
