using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Work.Data.Validation
{
    /// <summary>
    /// Custom attribute to ensure the value of a property is not null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get the Id of the object
            var instance = validationContext.ObjectInstance;
            var property = validationContext.ObjectType.GetProperty("Id");
            var id = property.GetValue(instance, null);

            // only validate if we are creating a new object
            // when updating, it's ok because the database value will be preserved
            if (id != null && ((int)id) == 0 && value == null)
                return new ValidationResult(string.Format("{0} should not be null.", validationContext.MemberName));
            return ValidationResult.Success;
        }
    }
}
