using FluentCMS.Data.Abstractions;
using System;
using System.Linq.Expressions;

namespace FluentCMS.Data.Common
{
    /// <summary>
    /// A builder class that allows for fluent creation of specifications
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class SpecificationBuilder<T> where T : class, IEntity
    {
        private readonly BaseSpecification<T> _specification;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationBuilder{T}"/> class
        /// </summary>
        /// <param name="specification">The specification to build upon</param>
        public SpecificationBuilder(BaseSpecification<T> specification)
        {
            _specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        /// <summary>
        /// Adds an include expression to the specification
        /// </summary>
        /// <param name="includeExpression">The include expression</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> Include(Expression<Func<T, object>> includeExpression)
        {
            ((dynamic)_specification).AddInclude(includeExpression);
            return this;
        }

        /// <summary>
        /// Adds an include string to the specification
        /// </summary>
        /// <param name="includeString">The include string</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> Include(string includeString)
        {
            ((dynamic)_specification).AddInclude(includeString);
            return this;
        }

        /// <summary>
        /// Applies paging to the specification
        /// </summary>
        /// <param name="skip">The number of elements to skip</param>
        /// <param name="take">The number of elements to take</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> ApplyPaging(int skip, int take)
        {
            ((dynamic)_specification).ApplyPaging(skip, take);
            return this;
        }

        /// <summary>
        /// Applies ordering to the specification
        /// </summary>
        /// <param name="orderByExpression">The order by expression</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> OrderBy(Expression<Func<T, object>> orderByExpression)
        {
            ((dynamic)_specification).ApplyOrderBy(orderByExpression);
            return this;
        }

        /// <summary>
        /// Applies descending ordering to the specification
        /// </summary>
        /// <param name="orderByDescendingExpression">The order by descending expression</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> OrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            ((dynamic)_specification).ApplyOrderByDescending(orderByDescendingExpression);
            return this;
        }

        /// <summary>
        /// Applies grouping to the specification
        /// </summary>
        /// <param name="groupByExpression">The group by expression</param>
        /// <returns>The specification builder for chaining</returns>
        public SpecificationBuilder<T> GroupBy(Expression<Func<T, object>> groupByExpression)
        {
            ((dynamic)_specification).ApplyGroupBy(groupByExpression);
            return this;
        }

        /// <summary>
        /// Gets the built specification
        /// </summary>
        /// <returns>The specification</returns>
        public ISpecification<T> Build()
        {
            return _specification;
        }
    }
}