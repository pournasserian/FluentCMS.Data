using System.Linq.Expressions;

namespace FluentCMS.Data.Abstractions.Extensions;

/// <summary>
/// Extension methods for IQueryable to abstract database-specific query operations
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Abstract Include method for eager loading related entities
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <typeparam name="TProperty">The included property type</typeparam>
    /// <param name="source">The source queryable</param>
    /// <param name="path">Path to the included property</param>
    /// <returns>Modified queryable with included entities</returns>
    public static IQueryable<T> Include<T, TProperty>(
        this IQueryable<T> source,
        Expression<Func<T, TProperty>> path)
    {
        // This is just a placeholder in the abstraction layer
        // The actual implementation will be provided by specific database providers
        return source;
    }

    /// <summary>
    /// Abstract string-based Include method for eager loading related entities
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="source">The source queryable</param>
    /// <param name="path">String path to the included property</param>
    /// <returns>Modified queryable with included entities</returns>
    public static IQueryable<T> Include<T>(
        this IQueryable<T> source,
        string path)
    {
        // This is just a placeholder in the abstraction layer
        // The actual implementation will be provided by specific database providers
        return source;
    }

    /// <summary>
    /// Abstract AsNoTracking method to disable change tracking
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="source">The source queryable</param>
    /// <returns>Modified queryable with change tracking disabled</returns>
    public static IQueryable<T> AsNoTracking<T>(
        this IQueryable<T> source)
    {
        // This is just a placeholder in the abstraction layer
        // The actual implementation will be provided by specific database providers
        return source;
    }

    /// <summary>
    /// Abstract OrderBy method to order entities by a key selector
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <param name="source">The source queryable</param>
    /// <param name="keySelector">Expression to select the key for ordering</param>
    /// <returns>Ordered queryable</returns>
    public static IQueryable<T> OrderBy<T, TKey>(
        this IQueryable<T> source,
        Expression<Func<T, TKey>> keySelector)
    {
        return source.OrderBy(keySelector);
    }

    /// <summary>
    /// Abstract OrderByDescending method to order entities by a key selector in descending order
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <param name="source">The source queryable</param>
    /// <param name="keySelector">Expression to select the key for ordering</param>
    /// <returns>Ordered queryable</returns>
    public static IQueryable<T> OrderByDescending<T, TKey>(
        this IQueryable<T> source,
        Expression<Func<T, TKey>> keySelector)
    {
        return source.OrderByDescending(keySelector);
    }
}
