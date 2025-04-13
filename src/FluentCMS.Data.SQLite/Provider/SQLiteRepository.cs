using FluentCMS.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentCMS.Data.SQLite.Provider
{
    /// <summary>
    /// SQLite implementation of the repository pattern
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class SQLiteRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly SQLiteDbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteRepository{T}"/> class
        /// </summary>
        /// <param name="context">The SQLite database context</param>
        public SQLiteRepository(SQLiteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <inheritdoc />
        public async Task<T> GetById(object id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(new[] { id }, cancellationToken);
            return entity ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found.");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> Find(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> SingleOrDefault(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var entity = await ApplySpecification(spec).SingleOrDefaultAsync(cancellationToken);
            return entity ?? throw new InvalidOperationException("No entity found matching the specification.");
        }

        /// <inheritdoc />
        public async Task<int> Count(ISpecification<T>? spec = null, CancellationToken cancellationToken = default)
        {
            if (spec == null)
            {
                return await _dbSet.CountAsync(cancellationToken);
            }
            
            return await ApplySpecification(spec).CountAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> Any(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).AnyAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> Add(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

        /// <inheritdoc />
        public async Task Update(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task Delete(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Applies a specification to the IQueryable
        /// </summary>
        /// <param name="spec">The specification to apply</param>
        /// <returns>The filtered IQueryable</returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}