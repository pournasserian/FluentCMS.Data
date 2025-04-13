using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentCMS.Data.Abstractions
{
    /// <summary>
    /// Interface for query specifications
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets the filter expression for the specification
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
        
        /// <summary>
        /// Gets the collection of include expressions for related entities
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        
        /// <summary>
        /// Gets the collection of include strings for related entities
        /// </summary>
        List<string> IncludeStrings { get; }
        
        /// <summary>
        /// Gets the ordering expression for the specification
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }
        
        /// <summary>
        /// Gets the descending ordering expression for the specification
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }
        
        /// <summary>
        /// Gets the grouping expression for the specification
        /// </summary>
        Expression<Func<T, object>>? GroupBy { get; }
        
        /// <summary>
        /// Gets the maximum number of results to return
        /// </summary>
        int Take { get; }
        
        /// <summary>
        /// Gets the number of results to skip
        /// </summary>
        int Skip { get; }
        
        /// <summary>
        /// Gets a value indicating whether paging is enabled
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}