namespace Work.Logic
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods applicable to all entities
    /// </summary>
    public static partial class ModelExtensions
    {

        /// <summary>
        /// Create a new instance of T from the source
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Tout To<Tin, Tout>(this Tin source)
            where Tin : class
            where Tout : class, new()
        {
            // create a new instance of type T
            var target = new Tout();

            // update the instance with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update object properties through reflection
        /// </summary>
        /// <param name="target">The object to be updated</param>
        /// <param name="source">The object with values to be used to update</param>
        /// <example>
        /// <code>
        ///     var po = GetPurchaseOrder(purchaseOrderId);
        ///     var supplier = GetSupplier(supplierId);
        ///     var model = new PurchaseOrderDetailModel();
        ///
        ///     model.UpdateFrom(po);
        ///     model.UpdateFrom(supplier);
        /// </code>
        /// </example>
        public static void UpdateFrom<T1, T2>(this T1 target, T2 source)
            where T1 : class
            where T2 : class
        {
            if (source == null) return;
            target.Update(source, target.GetType(), source.GetType());
        }

        public static void Initialize<T1>(this T1 target)
        {
            var targetType = target.GetType();

            // get all the properties that are writable
            var properties = targetType.GetProperties()
                                       .Where(p => p.CanWrite && !p.PropertyType.IsValueType);

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(target, null);

                // no need to proceed if property already contains a value
                if (propertyValue != null) continue;

                if (property.PropertyType.Equals(typeof(string)))
                {
                    // set string property to empty string
                    target.SetProperty(property, string.Empty);
                }
                else
                {
                    // create an instance for non-string properties 
                    propertyValue = Activator.CreateInstance(property.PropertyType);
                    // initialize the instance as well
                    propertyValue.Initialize();
                    // update the property
                    target.SetProperty(property, propertyValue);
                }
            }
        }
    }
}