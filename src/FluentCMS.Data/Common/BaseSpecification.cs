using FluentCMS.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentCMS.Data.Common
{
    /// <summary>
    /// Base implementation of ISpecification that can be inherited to create specific query specifications
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets the filter expression for the specification
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; private set; }
        
        /// <summary>
        /// Gets the collection of include expressions for related entities
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        
        /// <summary>
        /// Gets the collection of include strings for related entities
        /// </summary>
        public List<string> IncludeStrings { get; } = new List<string>();
        
        /// <summary>
        /// Gets the ordering expression for the specification
        /// </summary>
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        
        /// <summary>
        /// Gets the descending ordering expression for the specification
        /// </summary>
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        
        /// <summary>
        /// Gets the grouping expression for the specification
        /// </summary>
        public Expression<Func<T, object>>? GroupBy { get; private set; }
        
        /// <summary>
        /// Gets the maximum number of results to return
        /// </summary>
        public int Take { get; private set; }
        
        /// <summary>
        /// Gets the number of results to skip
        /// </summary>
        public int Skip { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether paging is enabled
        /// </summary>
        public bool IsPagingEnabled { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecification{T}"/> class
        /// </summary>
        protected BaseSpecification()
        {
            Criteria = _ => true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecification{T}"/> class with a condition
        /// </summary>
        /// <param name="criteria">The condition of the specification</param>
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Adds an include expression to the specification
        /// </summary>
        /// <param name="includeExpression">The include expression</param>
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Adds an include string to the specification
        /// </summary>
        /// <param name="includeString">The include string</param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        /// <summary>
        /// Applies paging to the specification
        /// </summary>
        /// <param name="skip">The number of elements to skip</param>
        /// <param name="take">The number of elements to take</param>
        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// Applies ordering to the specification
        /// </summary>
        /// <param name="orderByExpression">The order by expression</param>
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Applies descending ordering to the specification
        /// </summary>
        /// <param name="orderByDescendingExpression">The order by descending expression</param>
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        /// <summary>
        /// Applies grouping to the specification
        /// </summary>
        /// <param name="groupByExpression">The group by expression</param>
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}