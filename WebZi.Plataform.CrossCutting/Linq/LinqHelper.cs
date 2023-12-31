using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;

namespace WebZi.Plataform.CrossCutting.Linq
{
    public static class LinqHelper
    {
        [Flags]
        public enum LinqListFlags
        {
            Distinct = 1,
            OrderBy = 1 << 1,
            OrderByDesc = 1 << 2,
            ToEmptyIfNull = 1 << 3,
            ToLower = 1 << 4,
            ToNullIfWhiteSpace = 1 << 5,
            ToUpper = 1 << 6,
            RemoveIfNull = 1 << 7,
            RemoveIfNullOrWhiteSpace = 1 << 8,
            RemoveIfWhiteSpace = 1 << 9,
            Trim = 1 << 10
        }

        public static List<T> GetList<T>(List<T> list, LinqListFlags Flags) where T : class
        {
            if (list?.Count == 0)
            {
                return list;
            }

            if (typeof(T) == typeof(string))
            {
                List<T> nullList = (list as List<string>)
                    .Where(x => x == null)
                    .ToList() as List<T>;

                if ((Flags & LinqListFlags.ToLower) == LinqListFlags.ToLower)
                {
                    list = (list as List<string>).Where(x => x != null).ToList().ConvertAll(x => x.ToLower()).ToList() as List<T>;
                }
                else if ((Flags & LinqListFlags.ToUpper) == LinqListFlags.ToUpper)
                {
                    list = (list as List<string>).Where(x => x != null).ToList().ConvertAll(x => x.ToUpper()).ToList() as List<T>;
                }
                else if ((Flags & LinqListFlags.Trim) == LinqListFlags.Trim)
                {
                    list = (list as List<string>).Where(x => x != null).ToList().ConvertAll(x => x.Trim()).ToList() as List<T>;
                }

                if (nullList?.Count > 0)
                {
                    list.AddRange(nullList);
                }

                if ((Flags & LinqListFlags.RemoveIfNullOrWhiteSpace) == LinqListFlags.RemoveIfNullOrWhiteSpace)
                {
                    list = (list as List<string>).Where(x => !string.IsNullOrWhiteSpace(x)).ToList().ConvertAll(x => x.ToLowerTrim()).ToList() as List<T>;
                }
                else
                {
                    if ((Flags & LinqListFlags.RemoveIfNull) == LinqListFlags.RemoveIfNull)
                    {
                        list = (list as List<string>).Where(x => x != null).ToList().ConvertAll(x => x.ToLowerTrim()).ToList() as List<T>;
                    }

                    if ((Flags & LinqListFlags.RemoveIfWhiteSpace) == LinqListFlags.RemoveIfWhiteSpace)
                    {
                        list = (list as List<string>).Where(x => x.Trim() != string.Empty).ToList().ConvertAll(x => x.ToLowerTrim()).ToList() as List<T>;
                    }
                }

                if ((Flags & LinqListFlags.ToEmptyIfNull) == LinqListFlags.ToEmptyIfNull)
                {
                    list = (list as List<string>).ConvertAll(x => x.ToEmptyIfNull()).ToList() as List<T>;
                }
                else if ((Flags & LinqListFlags.ToNullIfWhiteSpace) == LinqListFlags.ToNullIfWhiteSpace)
                {
                    list = (list as List<string>).ConvertAll(x => x.ToNullIfEmpty()).ToList() as List<T>;
                }
            }

            if ((Flags & LinqListFlags.Distinct) == LinqListFlags.Distinct)
            {
                list = list.Distinct().ToList();
            }

            if ((Flags & LinqListFlags.OrderBy) == LinqListFlags.OrderBy)
            {
                list = list.Order().ToList();
            }
            else if ((Flags & LinqListFlags.OrderByDesc) == LinqListFlags.OrderByDesc)
            {
                list = list.OrderDescending().ToList();
            }

            return list;
        }

        public static List<T> GetDistinctList<T>(List<T> list) where T : class
        {
            return list?.Count == 0 ? list : list.Distinct().OrderBy(x => x).ToList();
        }

        public static List<T> GetOrderlyList<T>(List<T> list) where T : class
        {
            return list?.Count == 0 ? list : list.OrderBy(x => x).ToList();
        }

        public static int CountItens<T>(this ICollection<T> list) where T : class
        {
            return list == null ? 0 : list.Count;
        }

        public static int CountItens<T>(this List<T> list) where T : class
        {
            return list == null ? 0 : list.Count;
        }

        public static bool ContainsDuplicates<T>(this List<T> list)
        {
            HashSet<T> hash = new();

            return list.Any(item => !hash.Add(item));
        }

        public static bool ContainsNegativeOrZeroNumbers<T>(this List<T> list)
        {
            if (list == null || !list.IsNumericType())
            {
                return false;
            }

            foreach (T item in list)
            {
                if (Convert.ToDecimal(item) <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsNullOrWhiteSpaceValues(this List<string> list)
        {
            return list?.FirstOrDefault(x => x.IsNullOrWhiteSpace()) != null;
        }

        //public static bool HasNullValues<T>(this List<T> list) where T : class
        //{
        //    return list?.Where(x => x == null).ToList().Count > 0;
        //}

        //public static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, List<string> properties)
        //{
        //    return ApplyOrder<T>(source, properties, "OrderBy");
        //}

        //public static IOrderedQueryable<T> OrderByDescending<T>(IQueryable<T> source, List<string> properties)
        //{
        //    return ApplyOrder<T>(source, properties, "OrderByDescending");
        //}

        //public static IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> source, List<string> properties)
        //{
        //    return ApplyOrder<T>(source, properties, "ThenBy");
        //}

        //public static IOrderedQueryable<T> ThenByDescending<T>(IOrderedQueryable<T> source, List<string> properties)
        //{
        //    return ApplyOrder<T>(source, properties, "ThenByDescending");
        //}

        //private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, List<string> properties, string methodName)
        //{
        //    Type type = typeof(T);

        //    ParameterExpression Parameter = Expression.Parameter(type, "x");

        //    PropertyInfo PropertyInfo;

        //    Expression expr = Parameter;

        //    foreach (string property in properties)
        //    {
        //        PropertyInfo = type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        //        expr = Expression.Property(expr, PropertyInfo);

        //        type = PropertyInfo.PropertyType;
        //    }

        //    Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

        //    LambdaExpression lambda = Expression.Lambda(delegateType, expr, Parameter);

        //    object result = typeof(Queryable).GetMethods().Single(
        //            method => method.Name == methodName
        //                    && method.IsGenericMethodDefinition
        //                    && method.GetGenericArguments().Length == 2
        //                    && method.GetParameters().Length == 2)
        //            .MakeGenericMethod(typeof(T), type)
        //            .Invoke(null, new object[] { source, lambda });

        //    return (IOrderedQueryable<T>)result;
        //}
    }
}