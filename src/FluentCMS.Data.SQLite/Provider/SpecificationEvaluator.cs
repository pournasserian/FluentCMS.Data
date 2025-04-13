using FluentCMS.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FluentCMS.Data.SQLite.Provider
{
    /// <summary>
    /// Evaluates a specification and applies it to an IQueryable
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public static class SpecificationEvaluator<T> where T : class, IEntity
    {
        /// <summary>
        /// Applies a specification to an IQueryable
        /// </summary>
        /// <param name="inputQuery">The input query</param>
        /// <param name="specification">The specification to apply</param>
        /// <returns>The filtered IQueryable</returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // Apply the filter criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Include related entities
            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            query = specification.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            // Apply ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply grouping
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                            .Take(specification.Take);
            }

            return query;
        }
    }
}