using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Extensions;

public static class IQueryableExtensions
{
    /*
     * IQueryable<T> - вхідна колекція (з БД), до якої застосовується сортування.
     * IQueryObject queryObject - Об'єкт, який містить параметри запиту
     * Dictionary<string, Expression<Func<T, object>>> orderByExpressions - мапа з ключами сортування та відповідними виразами
     */
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> orderByExpressions)
    {
        if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !orderByExpressions.ContainsKey(queryObject.SortBy))
            return query;
        
        return queryObject.IsSortAscending ? query.OrderBy(orderByExpressions[queryObject.SortBy]) : query.OrderByDescending(orderByExpressions[queryObject.SortBy]);
    }
}