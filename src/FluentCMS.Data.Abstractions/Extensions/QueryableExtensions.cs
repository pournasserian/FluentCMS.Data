namespace FluentCMS.Data.Abstractions.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int skip, int take)
    {
        return query.Skip(skip).Take(take);
    }
}
