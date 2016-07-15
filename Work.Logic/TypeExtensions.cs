using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Work.Logic
{
    public static class TypeExtensions
    {
        // this allows case-insensitive searches for propeties/methods
        private static BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

        internal static void Update(this object target, object source, System.Type targetType, System.Type sourceType)
        {
            // get all the properties that are writable
            var properties = targetType.GetProperties()
                                       .Where(p => p.CanWrite);

            foreach (var targetProperty in properties)
            {
                var targetPropertyName = targetProperty.Name;
                // ensure the source has matching property
                var sourceProperty = sourceType.GetProperty(targetPropertyName, bindingFlags);
                if (sourceProperty != null)
                {
                    // get the value from source
                    object sourceValue = sourceProperty.GetValue(source, null);

                    // set the value on target
                    //targetProperty.SetValue(target, sourceValue, bindingFlags, null, null, null);
                    target.SetProperty(targetProperty, sourceValue);
                }
                else
                {
                    sourceProperty = sourceType.GetProperties()
                                               .FirstOrDefault(p => targetPropertyName.StartsWith(p.Name));
                    if (sourceProperty != null)
                    {
                        // get the value from source
                        object sourceValue = sourceProperty.GetValue(source, null);
                        var sourceValuePropertyName = targetPropertyName.Replace(sourceProperty.Name, string.Empty);
                        var sourceValueProperty = sourceValue.GetType().GetProperty(sourceValuePropertyName);

                        if (sourceValueProperty != null)
                        {
                            //targetProperty.SetValue(target, sourceValueProperty.GetValue(sourceValue, null),
                            //    bindingFlags, null, null, null);
                            target.SetProperty(targetProperty, sourceValueProperty.GetValue(sourceValue, null));
                        }
                    }
                }
            }
        }

        internal static void SetProperty<T1>(this T1 target, PropertyInfo property, object value)
        {
            var valueType = value?.GetType() ?? property.PropertyType;

            // check if the types are compatible
            if (property.PropertyType == valueType ||
                property.PropertyType.IsAssignableFrom(valueType))
            {
                // set the property value on target
                property.SetValue(target, value, bindingFlags, null, null, null);
            }
            // check if the types are convertible
            else if (property.PropertyType.IsValueType &&
                    valueType.CanConvertTo(property.PropertyType))
            {
                // set the property value on target
                // we may need to convert first?
                property.SetValue(target, value, bindingFlags, null, null, null);
            }
            else
            {
                // get the current value of the property
                // or create an instance of the property type
                var propertyValue = property.GetValue(target, null) ??
                                    Activator.CreateInstance(property.PropertyType);

                // update the value from the source object
                Update(propertyValue, value, property.PropertyType, valueType);

                // set the property value on target object
                property.SetValue(target, propertyValue, bindingFlags, null, null, null);
            }
        }
        private static bool CanConvertTo(this Type fromType, Type toType)
        {
            Type converterType = typeof(TypeConverterChecker<,>).MakeGenericType(fromType, toType);
            object instance = Activator.CreateInstance(converterType);
            return (bool)converterType.GetProperty("CanConvert").GetGetMethod().Invoke(instance, null);
        }

        private class TypeConverterChecker<TFrom, TTo>
        {
            public bool CanConvert { get; private set; }

            public TypeConverterChecker()
            {
                TFrom from = default(TFrom);
                if (from == null)
                    if (typeof(TFrom).Equals(typeof(String)))
                        from = (TFrom)(dynamic)"";
                    else
                        from = (TFrom)Activator.CreateInstance(typeof(TFrom));
                try
                {
                    TTo to = (dynamic)from;
                    CanConvert = true;
                }
                catch
                {
                    CanConvert = false;
                }
            }
        }
    }
}
