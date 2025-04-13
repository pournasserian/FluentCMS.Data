namespace FluentCMS.Data.Abstractions;

/// <summary>
/// Generic repository interface
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Gets an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The entity</returns>
    Task<T> GetById(object id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A collection of all entities</returns>
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Finds entities based on a specification
    /// </summary>
    /// <param name="spec">The specification to filter entities</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A collection of entities that match the specification</returns>
    Task<IEnumerable<T>> Find(ISpecification<T> spec, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a single entity based on a specification or returns null if no entity is found
    /// </summary>
    /// <param name="spec">The specification to filter entities</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The entity or null</returns>
    Task<T> SingleOrDefault(ISpecification<T> spec, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Counts entities based on a specification
    /// </summary>
    /// <param name="spec">The specification to filter entities</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The number of entities</returns>
    Task<int> Count(ISpecification<T>? spec = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if any entities match the specification
    /// </summary>
    /// <param name="spec">The specification to filter entities</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>True if any entities match the specification; otherwise, false</returns>
    Task<bool> Any(ISpecification<T> spec, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds an entity
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The added entity</returns>
    Task<T> Add(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds a range of entities
    /// </summary>
    /// <param name="entities">The entities to add</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The added entities</returns>
    Task<IEnumerable<T>> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task Update(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a range of entities
    /// </summary>
    /// <param name="entities">The entities to update</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task Delete(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes a range of entities
    /// </summary>
    /// <param name="entities">The entities to delete</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}