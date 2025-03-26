using System.Linq.Expressions;
using System.Reflection;

namespace CommonStockManagementDatabase.Model
{
    public class QueryHelper
    {
        // Sort and order assgin for query 
        public static IQueryable<TEntity> ApplySort<TEntity>(IQueryable<TEntity> query, string sort, string order, Expression<Func<TEntity, object>> defaultSort)
        {
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                var propertyInfo = typeof(TEntity).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "a");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

                    query = order.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                        ? query.OrderBy(lambda)
                        : query.OrderByDescending(lambda);
                }
            }
            else
            {
                query = query.OrderByDescending(defaultSort);
            }

            return query;
        }

        // Serach Funtion 
        public class SearchUtility<TEntity>
        {
            public static List<TEntity> Search(List<TEntity> data, string searchTerm, params Func<TEntity, string>[] fieldSelectors)
            {
                if (string.IsNullOrEmpty(searchTerm) || fieldSelectors == null || fieldSelectors.Length == 0)
                    return data;

                searchTerm = searchTerm.ToLower(); // Convert searchTerm to lowercase for case-insensitive search

                return data
                    .Where(item => fieldSelectors.Any(selector => selector(item).Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)))
                    .ToList();

            }
        }


    }
}
