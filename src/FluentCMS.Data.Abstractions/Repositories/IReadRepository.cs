using System.Linq.Expressions;
using FluentCMS.Data.Abstractions.Entities;
using FluentCMS.Data.Abstractions.Specifications;

namespace FluentCMS.Data.Abstractions.Repositories;

/// <summary>
/// Interface for read-only repository operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
/// <typeparam name="TKey">Entity key type</typeparam>
public interface IReadRepository<T, TKey> where T : IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets an entity by its primary key
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IReadOnlyList of entities</returns>
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets entities that match the provided specification
    /// </summary>
    /// <param name="specification">Specification with filtering, ordering, and including criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IReadOnlyList of entities that match the specification</returns>
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets entities that match the provided filter expression
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IReadOnlyList of entities that match the filter</returns>
    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the first entity that matches the provided specification or null if none is found
    /// </summary>
    /// <param name="specification">Specification with filtering, ordering, and including criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity that matches the specification or null</returns>
    Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the first entity that matches the provided filter expression or null if none is found
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity that matches the filter or null</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a single entity that matches the provided specification
    /// </summary>
    /// <param name="specification">Specification with filtering, ordering, and including criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Single entity that matches the specification</returns>
    /// <exception cref="InvalidOperationException">Thrown when multiple entities or no entities match the specification</exception>
    Task<T> SingleAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a single entity that matches the provided filter expression
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Single entity that matches the filter</returns>
    /// <exception cref="InvalidOperationException">Thrown when multiple entities or no entities match the filter</exception>
    Task<T> SingleAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a single entity that matches the provided specification or null if none is found
    /// </summary>
    /// <param name="specification">Specification with filtering, ordering, and including criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Single entity that matches the specification or null</returns>
    /// <exception cref="InvalidOperationException">Thrown when multiple entities match the specification</exception>
    Task<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a single entity that matches the provided filter expression or null if none is found
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Single entity that matches the filter or null</returns>
    /// <exception cref="InvalidOperationException">Thrown when multiple entities match the filter</exception>
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Counts the entities that match the provided specification
    /// </summary>
    /// <param name="specification">Specification with filtering criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Count of matching entities</returns>
    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Counts the entities that match the provided filter expression
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Count of matching entities</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Counts all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total count of entities</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if any entity matches the provided specification
    /// </summary>
    /// <param name="specification">Specification with filtering criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if any entity matches the specification, otherwise false</returns>
    Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if any entity matches the provided filter expression
    /// </summary>
    /// <param name="filter">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if any entity matches the filter, otherwise false</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if any entities exist
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if any entities exist, otherwise false</returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Interface for read-only repository operations with string key
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IReadRepository<T> : IReadRepository<T, string> where T : IEntity<string>
{
}
