using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> map) 
        {
            if (String.IsNullOrEmpty(queryObj.SortBy) || !map.ContainsKey(queryObj.SortBy)) 
                return query;
            if (queryObj.IsSortingDescending) {
                return query.OrderByDescending(map[queryObj.SortBy]);
            } else {
                return query.OrderBy(map[queryObj.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj) 
        {
            if (queryObj.Page <= 0) {
                queryObj.Page = 1;
            }
            if (queryObj.PageSize <= 0) {
                queryObj.PageSize = 10;
            }
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}