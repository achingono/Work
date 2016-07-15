using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Work.Logic
{
    public static class ExpressionExtensions
    {
        // this allows case-insensitive searches for propeties/methods
        private static BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

        // cache the FilterModel type
        private static Type filterModelType = typeof(FilterModel);

        // Gets Queryable.Where<TSource>(IQueryable<TSource>, Expression<Func<TSource, bool>>)
        private static MethodInfo whereMethod = typeof(Queryable).GetMethods().First(m => m.Name == "Where" && m.GetParameters().Length == 2);


        /// <summary>
        /// Create a lambda expression used to filter data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> CreateLambda<TEntity, TModel>(this TModel model)
        {
            if (model == null)
            {
                return null;
            }

            // the type of entity to run query against
            var entityType = typeof(TEntity);

            // Expression: "entity"
            var parameter = Expression.Parameter(entityType, "entity");
            var memberExpressions = model.CreateExpressions(entityType, parameter);
            Expression comparison = null;

            // Combine multiple expressions into a single conditional expression
            foreach (Expression expression in memberExpressions)
            {
                if (comparison == null)
                {
                    // Expression: "entity.PropertyName == value
                    comparison = expression;
                }
                else if (expression != null)
                {
                    // Expression: "entity.PropertyName == value || entity.PropertyName == value"
                    comparison = Expression.AndAlso(comparison, expression);
                }
            }

            if (comparison == null)
            {
                return null;
            }

            // Expression: "entity => entity.PropertyName.Equals(value) || entity.PropertyName.StartsWith(value) || ..."
            return Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
        }

        /// <summary>
        /// Create a list of comparisons to be grouped together to form a lambda expression
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="parameter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IEnumerable<Expression> CreateExpressions<TModel>(this TModel model, Type entityType, ParameterExpression parameter)
        {
            // get only properties that return 'FilterModel'
            var modelProperties = typeof(TModel).GetProperties(bindingFlags);

            // create an expression for each property
            foreach (var modelProperty in modelProperties)
            {
                if (modelProperty.PropertyType == filterModelType)
                {
                    yield return CreateFilterExpression(model, modelProperty, entityType, parameter);
                }
                else
                {
                    yield return CreateIdentityExpression(model, modelProperty, entityType, parameter);
                }
            }
        }

        /// <summary>
        /// Create an expression that calls a method on the value of the property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="property"></param>
        /// <param name="entityType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static MethodCallExpression CreateFilterExpression<TModel>(this TModel model, PropertyInfo property, Type entityType, ParameterExpression parameter)
        {
            var filter = property.GetValue(model) as FilterModel;

            // skip if no filter value was supplied
            if (filter == null || string.IsNullOrWhiteSpace(filter.Match))
            {
                return null;
            }

            // Expression: "entity.PropertyName"
            var expression = GetPropertyExpression(entityType, property.Name, parameter);

            // skip if no valid expression could be created
            if (expression == null)
            {
                return null;
            }

            ConstantExpression comparable = null;
            try
            {
                // Expression: "value"
                comparable = Expression.Constant(
                                    Convert.ChangeType(filter.Match, expression.Type)
                                );
            }
            catch (Exception)
            {
                // invalid state
                return null;
            }

            // get the chosen comparison/filter method
            var tuple = GetMethod(expression.Type, filter.Filter);

            if (tuple != null && tuple.Item1 != null && tuple.Item2 == 2)
            {
                // Expression: "entity.PropertyName.Equals/StartsWith/Contains(value, StringComparison.IgnoreCase)"
                return Expression.Call(expression, tuple.Item1, new[] {
                        comparable,
                        Expression.Constant(StringComparison.OrdinalIgnoreCase)
                    });
            }
            else if (tuple != null && tuple.Item1 != null && tuple.Item2 == 1)
            {
                // Expression: "entity.PropertyName.Equals/StartsWith/Contains(value)"
                return Expression.Call(expression, tuple.Item1, comparable);
            }

            // invalid state
            return null;
        }

        /// <summary>
        /// Create an expression that compares values of the identity properties
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="modelProperty"></param>
        /// <param name="entityType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static BinaryExpression CreateIdentityExpression<TModel>(this TModel model, PropertyInfo modelProperty, Type entityType, ParameterExpression parameter)
        {
            // ensure Model property has sub-property called "Id"
            var idProperty = modelProperty.PropertyType.GetProperty("Id", bindingFlags);
            if (idProperty == null)
            {
                return null;
            }
            // ensure entity has matching property
            var entityProperty = entityType.GetProperty(modelProperty.Name, bindingFlags);
            if (entityProperty == null)
            {
                return null;
            }

            // ensure entity property also has sub-property called "Id"
            var entityIdProperty = entityProperty.PropertyType.GetProperty("Id", bindingFlags);
            if (entityIdProperty == null || !entityIdProperty.PropertyType.IsAssignableFrom(idProperty.PropertyType))
            {
                return null;
            }

            // ensure the model property is not null
            var modelPropertyValue = modelProperty.GetValue(model);
            if (modelPropertyValue == null)
            {
                return null;
            }

            // ensure the identity property is not null
            var modelPropertyIdValue = idProperty.GetValue(modelPropertyValue);
            if (modelPropertyValue == null)
            {
                return null;
            }

            // Expression: entity.Property.Id == {Id}
            return Expression.Equal(
                Expression.Property(
                    Expression.Property(parameter, entityProperty),
                    entityIdProperty
                ),
                Expression.Constant(
                    Convert.ChangeType(modelPropertyIdValue, entityIdProperty.PropertyType)
                )
            );
        }

        /// <summary>
        /// Generate a MemberExpression that accesses an entity property
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static MemberExpression GetPropertyExpression(Type type, string propertyName, ParameterExpression parameter)
        {
            // retrive property information
            PropertyInfo parentProperty = type.GetProperty(propertyName, bindingFlags);
            PropertyInfo childProperty = null;

            if (parentProperty == null)
            {
                // find properties whose name
                // starts with the given property name
                var matchedProperties = type.GetProperties(bindingFlags)
                                            .Where(p => propertyName.StartsWith(p.Name));

                // create an expression for the first valid property
                foreach (var matchedProperty in matchedProperties)
                {
                    // remove the first part of the property name that was matched
                    var subPropertyName = propertyName.Substring(matchedProperty.Name.Length);
                    // attempt to retrieve the sub-property
                    childProperty = matchedProperty.PropertyType.GetProperty(subPropertyName, bindingFlags);

                    // make sure the sub-property exists
                    if (childProperty != null)
                    {
                        parentProperty = matchedProperty;
                        break;
                    }
                }
            }

            // generate expression from parameter
            return GetPropertyExpression(parentProperty, childProperty, parameter);
        }

        /// <summary>
        /// Generate a MemberExpression that accesses an entity property
        /// </summary>
        /// <param name="parentProperty">The property of the entity</param>
        /// <param name="childProperty">The property of the property</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static MemberExpression GetPropertyExpression(PropertyInfo parentProperty, PropertyInfo childProperty, ParameterExpression parameter)
        {
            if (parentProperty != null && childProperty != null)
            {
                // Expression: entity.Property.Property
                return Expression.Property(
                    // Expression: entity.Property
                        Expression.Property(parameter, parentProperty),
                        childProperty
                    );
            }
            else if (parentProperty != null)
            {
                return Expression.Property(parameter, parentProperty);
            }

            return null;
        }

        /// <summary>
        /// Get the method which corresponds to the specified filter expression
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Tuple<MethodInfo, int> GetMethod(Type type, SearchFilter filter)
        {
            var methodName = Enum.GetName(typeof(SearchFilter), filter);
            var parameterCount = 2;
            // the SearchFilter value must match
            // a method of the String type
            // which accepts two parameters
            var method = type.GetMethod(
                            methodName,
                            new Type[] {
                                type ,
                                typeof(StringComparison)
                            }
                        );

            // the 'StartsWith' method throws an exception
            if (method == null || filter == SearchFilter.StartsWith)
            {
                // A valid method could not be found
                // try to locate one that accepts only a single parameter
                method = type.GetMethod(
                                methodName,
                                new[] {
                                    type
                                }
                            );
                parameterCount = 1;
            }

            return Tuple.Create(method, parameterCount);
        }
    }
}