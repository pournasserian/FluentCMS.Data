# FluentCMS.Data.Abstractions

Core abstractions for the FluentCMS data access layer that define the interfaces, base classes, and patterns used across all database providers.

## Overview

This package contains the foundational types needed to implement the Repository and Unit of Work patterns in FluentCMS. It provides a clean separation between business logic and data access logic by defining provider-independent interfaces.

## Components

### Entities

- `IEntity<TKey>` - Base entity interface with generic ID type
- `IEntity` - Specialized version with string ID type
- `IAuditableEntity` - Interface for entities that need audit tracking
- `BaseEntity<TKey>` - Abstract implementation of IEntity
- `BaseEntity` - Specialized version with string ID type

### Repositories

- `IReadRepository<T, TKey>` - Read-only repository operations
- `IReadRepository<T>` - Specialized version with string ID type
- `IRepository<T, TKey>` - Full CRUD repository operations 
- `IRepository<T>` - Specialized version with string ID type
- `IUnitOfWork` - Transaction management and repository access

### Specifications

The specification pattern allows for encapsulating and reusing query logic:

- `ISpecification<T>` - Core specification interface
- `BaseSpecification<T>` - Base implementation with common functionalities
- `SpecificationEvaluator<T, TKey>` - Applies specifications to queries

### Configuration

- `DataProviderType` - Enum of supported database providers
- `DataOptions` - Base configuration options class

### Extensions

- `QueryableExtensions` - LINQ extension methods to abstract provider-specific query capabilities

## Usage

This package is referenced by all FluentCMS database provider implementations and by application code that needs to work with repositories.

```csharp
// Example usage pattern
public class UserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}
```
