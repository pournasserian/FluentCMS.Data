# FluentCMS.Data - Product Requirements Document (PRD)

## 1. Introduction

### 1.1 Purpose
FluentCMS.Data is a database abstraction layer designed to provide a consistent data access pattern for FluentCMS applications. It implements the Repository pattern to separate data access logic from business logic and supports multiple database providers.

### 1.2 Scope
This library will handle all database interactions for FluentCMS applications, allowing developers to focus on business logic rather than data access concerns. It provides a common interface across various database providers.

### 1.3 Target Users
- FluentCMS developers
- Client applications built on FluentCMS
- Third-party extensions and plugins for FluentCMS

## 2. Product Overview

### 2.1 Product Perspective
FluentCMS.Data serves as the data access layer for FluentCMS applications. It sits between the application's business logic and the underlying database, providing a clean abstraction to interact with data.

### 2.2 Key Features
- Repository pattern implementation
- Support for multiple database providers (starting with SQLite via EF)
- Specification pattern for query logic
- Pagination, sorting, and filtering support
- Async operations with cancellation token support
- Audit trail tracking

### 2.3 Assumptions and Dependencies
- .NET 8 or later
- Entity Framework Core for relational database providers
- Provider-specific dependencies (e.g., Microsoft.EntityFrameworkCore.Sqlite for SQLite)

## 3. Functional Requirements

### 3.1 Core Repository Interface
- Define `IRepository<T>` interface with CRUD operations
- Support async operations for all methods (without "Async" suffix)
- Implement specification pattern for querying data
- All methods should include cancellation token support

### 3.2 Database Providers
#### 3.2.1 SQLite Provider (Priority 1)
- Implement using Entity Framework Core
- Support SQLite-specific optimizations and features

#### 3.2.2 Other Providers (Future)
- SQL Server
- PostgreSQL
- MongoDB
- LiteDB
- MySQL

### 3.3 Configuration
- Runtime provider selection
- Connection string configuration
- Migration support
- Entity configuration

### 3.4 Query Capabilities
- Filtering with specifications
- Sorting (single and multi-field)
- Pagination with skip/take semantics
- Projection to DTOs

### 3.5 Auditing
- Track creation/modification timestamps
- Record user information for changes

## 4. Technical Requirements

### 4.1 Architecture
```
FluentCMS.Data (Core package)
├── Abstractions
│   ├── IRepository<T>
│   ├── ISpecification<T>
│   └── Configuration interfaces
├── Common
│   ├── Base entity classes
│   ├── Specification implementations
│   └── Pagination and sorting utilities
└── Extensions
    └── Service registration extensions

FluentCMS.Data.SQLite (Provider package)
├── SQLiteRepository<T>
├── SQLiteDbContext
└── SQLiteServiceExtensions
```

### 4.2 Performance Requirements
- Efficient query execution
- Connection pooling
- Minimal memory footprint
- Support for handling large datasets through pagination

### 4.3 Security Requirements
- Parameterized queries to prevent SQL injection
- Support for connection string encryption
- No hardcoded credentials

### 4.4 Testing Requirements
- Unit tests for core functionality
- Integration tests with actual database instances
- Performance benchmarks

## 5. API Design

### 5.1 Core Interfaces

```csharp
public interface IRepository<T> where T : class, IEntity
{
    Task<T> GetById(object id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> Find(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<T> SingleOrDefault(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<int> Count(ISpecification<T> spec = null, CancellationToken cancellationToken = default);
    Task<bool> Any(ISpecification<T> spec, CancellationToken cancellationToken = default);
    
    Task<T> Add(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    Task Update(T entity, CancellationToken cancellationToken = default);
    Task UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    Task Delete(T entity, CancellationToken cancellationToken = default);
    Task DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
```

### 5.2 Configuration

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentCmsData(
        this IServiceCollection services, 
        Action<FluentCmsDataOptions> configureOptions)
    {
        // Configuration logic here
        return services;
    }
}

public class FluentCmsDataOptions
{
    public string ConnectionString { get; set; }
    public void UseProvider<TProvider>() where TProvider : class, IDataProvider;
    // Additional configuration options
}
```

## 6. Documentation

### 6.1 API Documentation
- XML comments for all public APIs
- Generated documentation website

### 6.2 Usage Examples
- Basic CRUD operations
- Working with specifications
- Provider configuration

#### 6.2.1 SQLite Provider Configuration

```csharp
// Register services in your application startup
public void ConfigureServices(IServiceCollection services)
{
    // Option 1: Using direct SQLite extension method
    services.AddFluentCmsSQLiteData("Data Source=fluentcms.db");
    
    // Option 2: Using generic provider configuration
    services.AddFluentCmsData(options => 
    {
        options.UseProvider<SQLiteProvider>();
        options.ConnectionString = "Data Source=fluentcms.db";
    });
}

// Use repositories in your services/controllers
public class ProductService
{
    private readonly IRepository<Product> _productRepository;
    
    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> GetProductById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetById(id, cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetActiveProducts(CancellationToken cancellationToken = default)
    {
        var spec = new ProductSpecification()
            .WhereIsActive()
            .OrderByNameAscending();
        
        return await _productRepository.Find(spec, cancellationToken);
    }
    
    public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default)
    {
        return await _productRepository.Add(product, cancellationToken);
    }
}
```

#### 6.2.2 Using Specifications

```csharp
// Define a specification for a Product entity
public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification()
    {
        // Default empty specification
    }
    
    public ProductSpecification(Guid id)
        : base(p => p.Id == id)
    {
    }
    
    public ProductSpecification WhereIsActive()
    {
        AddCriteria(p => p.IsActive);
        return this;
    }
    
    public ProductSpecification WhereCategory(Guid categoryId)
    {
        AddCriteria(p => p.CategoryId == categoryId);
        return this;
    }
    
    public ProductSpecification WithCategory()
    {
        AddInclude(p => p.Category);
        return this;
    }
    
    public ProductSpecification OrderByNameAscending()
    {
        ApplyOrderBy(p => p.Name);
        return this;
    }
    
    public ProductSpecification OrderByPriceDescending()
    {
        ApplyOrderByDescending(p => p.Price);
        return this;
    }
    
    public ProductSpecification ApplyPaging(int pageNumber, int pageSize)
    {
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        return this;
    }
}

// Using the specification with repository
public async Task<PagedResult<Product>> GetProductsByCategoryPaged(
    Guid categoryId, 
    int pageNumber, 
    int pageSize, 
    CancellationToken cancellationToken = default)
{
    var spec = new ProductSpecification()
        .WhereCategory(categoryId)
        .WithCategory()
        .OrderByNameAscending()
        .ApplyPaging(pageNumber, pageSize);
    
    var items = await _repository.Find(spec, cancellationToken);
    var totalCount = await _repository.Count(new ProductSpecification().WhereCategory(categoryId), cancellationToken);
    
    return new PagedResult<Product>(items, totalCount, pageNumber, pageSize);
}
```

### 6.3 Tutorials
- Getting started guide
- Migration from direct database access
- Creating custom specifications