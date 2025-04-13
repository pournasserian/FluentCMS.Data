using System.Linq;
using FluentCMS.Data.Abstractions.Entities;
using FluentCMS.Data.Abstractions.Extensions;

namespace FluentCMS.Data.Abstractions.Specifications;

/// <summary>
/// Helper class to evaluate specifications
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
/// <typeparam name="TKey">Entity key type</typeparam>
public class SpecificationEvaluator<T, TKey> where T : IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Applies a specification to an IQueryable
    /// </summary>
    /// <param name="inputQuery">The input query</param>
    /// <param name="specification">The specification to apply</param>
    /// <returns>The modified query with the specification applied</returns>
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        // Apply criteria filter if provided
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply ordering
        if (specification.OrderBy.Any())
        {
            foreach (var (keySelector, descending) in specification.OrderBy)
            {
                query = descending 
                    ? query.OrderByDescending(keySelector) 
                    : query.OrderBy(keySelector);
            }
        }

        // Apply grouping
        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // Apply includes for eager loading
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        
        // Apply string-based includes
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        // Apply pagination
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        // Apply tracking behavior
        if (specification.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}

/// <summary>
/// Helper class to evaluate specifications for entities with string keys
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class SpecificationEvaluator<T> : SpecificationEvaluator<T, string> where T : IEntity<string>
{
}
