using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Work.Data.Validation
{
    /// <summary>
    /// Custom attribute to set value to DateTime.Now if value is null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NowAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null
                || ((DateTime)value) == DateTime.MinValue)
            {
                var instance = validationContext.ObjectInstance;
                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);

                property.SetValue(instance, DateTime.Now, null);
            }
            return ValidationResult.Success;
        }
    }
}
