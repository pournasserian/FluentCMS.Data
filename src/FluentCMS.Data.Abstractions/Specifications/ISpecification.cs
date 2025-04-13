using System.Linq.Expressions;

namespace FluentCMS.Data.Abstractions.Specifications;

/// <summary>
/// Interface for implementing the specification pattern
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the filter expression
    /// </summary>
    Expression<Func<T, bool>>? Criteria { get; }
    
    /// <summary>
    /// Gets the collection of include expressions
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }
    
    /// <summary>
    /// Gets the collection of string-based include expressions
    /// </summary>
    List<string> IncludeStrings { get; }
    
    /// <summary>
    /// Gets the collection of order by expressions
    /// </summary>
    List<(Expression<Func<T, object>> KeySelector, bool Descending)> OrderBy { get; }
    
    /// <summary>
    /// Gets the number of elements to skip
    /// </summary>
    int Skip { get; }
    
    /// <summary>
    /// Gets the number of elements to take
    /// </summary>
    int Take { get; }
    
    /// <summary>
    /// Gets a value indicating whether pagination is enabled
    /// </summary>
    bool IsPagingEnabled { get; }
    
    /// <summary>
    /// Gets the collection of grouped expressions
    /// </summary>
    Expression<Func<T, object>>? GroupBy { get; }
    
    /// <summary>
    /// Gets whether related entities should be eagerly loaded
    /// </summary>
    bool AsNoTracking { get; }
}
