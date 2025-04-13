using System.Linq.Expressions;

namespace FluentCMS.Data.Abstractions.Specifications;

/// <summary>
/// Base implementation of the specification pattern
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public abstract class BaseSpecification<T> : ISpecification<T>
{
    /// <summary>
    /// Gets the filter expression
    /// </summary>
    public Expression<Func<T, bool>>? Criteria { get; protected set; }
    
    /// <summary>
    /// Gets the collection of include expressions
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    
    /// <summary>
    /// Gets the collection of string-based include expressions
    /// </summary>
    public List<string> IncludeStrings { get; } = new();
    
    /// <summary>
    /// Gets the collection of order by expressions
    /// </summary>
    public List<(Expression<Func<T, object>> KeySelector, bool Descending)> OrderBy { get; } = new();
    
    /// <summary>
    /// Gets the number of elements to skip
    /// </summary>
    public int Skip { get; protected set; }
    
    /// <summary>
    /// Gets the number of elements to take
    /// </summary>
    public int Take { get; protected set; }
    
    /// <summary>
    /// Gets a value indicating whether pagination is enabled
    /// </summary>
    public bool IsPagingEnabled { get; protected set; }
    
    /// <summary>
    /// Gets the collection of grouped expressions
    /// </summary>
    public Expression<Func<T, object>>? GroupBy { get; protected set; }
    
    /// <summary>
    /// Gets whether related entities should be eagerly loaded
    /// </summary>
    public bool AsNoTracking { get; protected set; }
    
    /// <summary>
    /// Initializes a new instance of the BaseSpecification class
    /// </summary>
    protected BaseSpecification()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the BaseSpecification class with a filter expression
    /// </summary>
    /// <param name="criteria">Filter expression</param>
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    /// <summary>
    /// Adds an include expression to the specification
    /// </summary>
    /// <param name="includeExpression">Include expression</param>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    /// <summary>
    /// Adds a string-based include to the specification
    /// </summary>
    /// <param name="includeString">Include string</param>
    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
    
    /// <summary>
    /// Applies pagination to the specification
    /// </summary>
    /// <param name="skip">Number of elements to skip</param>
    /// <param name="take">Number of elements to take</param>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
    
    /// <summary>
    /// Applies ordering to the specification
    /// </summary>
    /// <param name="orderByExpression">Order by expression</param>
    /// <param name="descending">Whether to order descending</param>
    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression, bool descending = false)
    {
        OrderBy.Add((orderByExpression, descending));
    }
    
    /// <summary>
    /// Applies ascending ordering to the specification
    /// </summary>
    /// <param name="orderByExpression">Order by expression</param>
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        ApplyOrderBy(orderByExpression);
    }
    
    /// <summary>
    /// Applies descending ordering to the specification
    /// </summary>
    /// <param name="orderByExpression">Order by expression</param>
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        ApplyOrderBy(orderByExpression, true);
    }
    
    /// <summary>
    /// Applies grouping to the specification
    /// </summary>
    /// <param name="groupByExpression">Group by expression</param>
    protected void AddGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
    
    /// <summary>
    /// Disables change tracking for query results
    /// </summary>
    protected void AsNoTrackingQuery()
    {
        AsNoTracking = true;
    }
}
