using System.Linq.Expressions;

namespace FluentCMS.Data.Abstractions.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification)
        where T : class
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.IsPagingEnabled)
        {
            if (specification.Skip.HasValue)
                query = query.Skip(specification.Skip.Value);
            if (specification.Take.HasValue)
                query = query.Take(specification.Take.Value);
        }

        return query;
    }

    private static IQueryable<T> Include<T>(this IQueryable<T> source, Expression<Func<T, object>> path)
    {
        return source; // Placeholder for actual Include logic in provider-specific implementation
    }

    private static IQueryable<T> Include<T>(this IQueryable<T> source, string path)
    {
        return source; // Placeholder for actual Include logic in provider-specific implementation
    }
}
