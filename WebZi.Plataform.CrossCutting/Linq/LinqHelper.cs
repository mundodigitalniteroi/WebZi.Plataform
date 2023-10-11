using System.Linq.Expressions;
using System.Reflection;

namespace WebZi.Plataform.CrossCutting.Linq
{
    public static class LinqHelper
    {
        public static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, List<string> properties)
        {
            return ApplyOrder<T>(source, properties, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(IQueryable<T> source, List<string> properties)
        {
            return ApplyOrder<T>(source, properties, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> source, List<string> properties)
        {
            return ApplyOrder<T>(source, properties, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(IOrderedQueryable<T> source, List<string> properties)
        {
            return ApplyOrder<T>(source, properties, "ThenByDescending");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, List<string> properties, string methodName)
        {
            Type type = typeof(T);

            ParameterExpression Parameter = Expression.Parameter(type, "x");

            PropertyInfo PropertyInfo;

            Expression expr = Parameter;

            foreach (string property in properties)
            {
                PropertyInfo = type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                expr = Expression.Property(expr, PropertyInfo);

                type = PropertyInfo.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            LambdaExpression lambda = Expression.Lambda(delegateType, expr, Parameter);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }
    }
}