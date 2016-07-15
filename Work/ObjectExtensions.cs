using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;

namespace Work
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the supplied <see cref="Object"/> into an <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="source">The <see cref="Object"/> to be converted</param>
        /// <param name="valuePropertiesOnly">Determines whether to include properties whose return type is not value type</param>
        /// <returns>A dynamic object</returns>
        public static dynamic ToExpando(this object source, bool valuePropertiesOnly = false)
        {
            var result = new ExpandoObject();
            var dictionary = result as IDictionary<string, object>; //work with the Expando as a Dictionary
            if (source.GetType() == typeof(ExpandoObject)) return source; //shouldn't have to... but just in case
            if (source.GetType() == typeof(NameValueCollection) || source.GetType().IsSubclassOf(typeof(NameValueCollection)))
            {
                var collection = (NameValueCollection)source;
                collection.Cast<string>().Select(key => new KeyValuePair<string, object>(key, collection[key])).ToList().ForEach(i => dictionary.Add(i));
            }
            else
            {
                var properties = source.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var type = property.PropertyType;
                    if (!valuePropertiesOnly || type.IsValueType || type.Equals(typeof(string)))
                    {
                        dictionary.Add(property.Name, property.GetValue(source, null));
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// Converts the supplied <see cref="Object"/> into a dictionary
        /// </summary>
        /// <param name="source">The <see cref="Object"/> to be converted</param>
        /// <returns>A <see cref="IDictionary"/> object with properties as keys</returns>
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return (IDictionary<string, object>)source.ToExpando();
        }

        /// <summary>
        /// Returns the JSON representation of an object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToJson(this object entity)
        {
            return JsonConvert.SerializeObject(entity, new IsoDateTimeConverter());
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static T FromJson<T>(this string json)
        {
            object value = JsonConvert.DeserializeObject<T>(json);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
