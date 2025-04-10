# FluentCMS.Data

A modular, provider-independent database abstraction layer for FluentCMS, designed to provide a consistent data access pattern across different applications and database providers.

## Overview

This project implements the Repository and Unit of Work patterns to create a clean separation between the data access logic and business logic of FluentCMS. The abstraction layer supports multiple database providers (SQL Server, PostgreSQL, SQLite, MongoDB, LiteDB) and can be extended to support additional providers as needed.

## Purpose

- **Reusability**: Ensure the data access code can be shared across different FluentCMS applications
- **Flexibility**: Support different database technologies through a common interface
- **Testability**: Make it easy to substitute real repositories with mock implementations for testing
- **Maintainability**: Centralize data access code in a single library to simplify maintenance

## Project Structure

The solution is structured as multiple projects to ensure clean separation of dependencies:

```
FluentCMS.Data/
├── FluentCMS.Data.Abstractions/   # Core abstractions and interfaces
│   ├── Entities/                  
│   │   ├── BaseEntity.cs          # Base entity that all domain entities inherit from
│   │   ├── IAuditableEntity.cs    # Interface for entities that need audit information
│   │   └── IEntity.cs             # Base entity interface
│   ├── Repositories/              # Repository interfaces
│   │   ├── IRepository.cs         # Generic repository interface
│   │   ├── IReadRepository.cs     # Read-only repository interface
│   │   └── IUnitOfWork.cs         # Unit of work interface
│   ├── Specifications/            # Specification pattern implementation
│   │   ├── ISpecification.cs      # Base specification interface
│   │   ├── BaseSpecification.cs   # Base specification implementation
│   │   └── SpecificationEvaluator.cs # Evaluator for specifications
│   ├── Configuration/
│   │   ├── DataOptions.cs         # Common configuration options
│   │   └── DataProviderType.cs    # Enum for database provider types
│   └── Extensions/
│       └── QueryableExtensions.cs # Common LINQ extensions
│
├── FluentCMS.Data/                # Main package for dynamic provider loading
│   ├── Extensions/
│   │   └── ServiceCollectionExtensions.cs # DI registration for all providers
│   └── Configuration/
│       ├── FluentCmsDataOptions.cs # Combined options for all providers
│       └── ProviderFactory.cs     # Factory for creating provider-specific instances
│
├── FluentCMS.Data.EntityFramework/ # EF Core implementation
│   ├── Repositories/
│   │   ├── EfRepository.cs        # EF Core repository implementation
│   │   └── EfReadRepository.cs    # EF Core read repository implementation
│   ├── UnitOfWork/
│   │   └── EfUnitOfWork.cs        # EF Core unit of work implementation
│   ├── Context/
│   │   └── EfDbContext.cs         # Base DbContext for EF Core
│   ├── Configuration/
│   │   └── EfDataOptions.cs       # EF Core specific options
│   └── Extensions/
│       └── EfServiceCollectionExtensions.cs # EF Core DI registration extensions
│
├── FluentCMS.Data.MongoDB/        # MongoDB implementation
│   ├── Repositories/
│   │   ├── MongoRepository.cs     # MongoDB repository implementation
│   │   └── MongoReadRepository.cs # MongoDB read repository implementation
│   ├── UnitOfWork/
│   │   └── MongoUnitOfWork.cs     # MongoDB unit of work implementation
│   ├── Context/
│   │   └── MongoDbContext.cs      # MongoDB context
│   ├── Configuration/
│   │   └── MongoDataOptions.cs    # MongoDB specific options
│   └── Extensions/
│       └── MongoServiceCollectionExtensions.cs # MongoDB DI registration extensions
│
└── FluentCMS.Data.LiteDB/         # LiteDB implementation
    ├── Repositories/
    │   ├── LiteRepository.cs      # LiteDB repository implementation
    │   └── LiteReadRepository.cs  # LiteDB read repository implementation
    ├── UnitOfWork/
    │   └── LiteUnitOfWork.cs      # LiteDB unit of work implementation
    ├── Context/
    │   └── LiteDbContext.cs       # LiteDB context
    ├── Configuration/
    │   └── LiteDataOptions.cs     # LiteDB specific options
    └── Extensions/
        └── LiteServiceCollectionExtensions.cs # LiteDB DI registration extensions
```

## Features

- **Generic Repository Pattern**: Common interface for working with data regardless of the underlying database
- **Unit of Work**: Maintains a list of objects affected by a business transaction and coordinates changes
- **Specification Pattern**: Encapsulates query logic to make it reusable and composable
- **Pagination Support**: Built-in support for paging through large datasets
- **Audit Trails**: Automatic tracking of creation and modification timestamps
- **Transaction Support**: Ensures data consistency across multiple operations
- **Provider-Specific Optimizations**: Each provider implementation can leverage database-specific features while maintaining the common interface

## Getting Started

### Installation

Install only the packages you need:

```bash
# Install the core abstractions (required)
dotnet add package FluentCMS.Data.Abstractions

# Install the database provider you want to use
dotnet add package FluentCMS.Data.EntityFramework
# or
dotnet add package FluentCMS.Data.MongoDB
# or
dotnet add package FluentCMS.Data.LiteDB
```

## Dynamic Database Provider Configuration

FluentCMS.Data supports runtime selection of database providers through configuration, allowing you to switch databases without rebuilding your application.

### Configuration in appsettings.json

```json
{
  "FluentCMS": {
    "Data": {
      "Provider": "EntityFramework", // Options: "EntityFramework", "MongoDB", "LiteDB"
      "ConnectionString": "Server=localhost;Database=FluentCMS;User Id=sa;Password=yourStrong(!)Password;",
      "Options": {
        // Provider-specific options
        "EnableSensitiveDataLogging": false,        // EntityFramework specific
        "CommandTimeout": 30,                       // EntityFramework specific
        "DatabaseName": "FluentCMS",                // MongoDB specific
        "EnableAutoIndexCreation": true,            // MongoDB specific
        "Password": "mypassword",                   // LiteDB specific
        "ConnectionMode": "Shared"                  // LiteDB specific
      }
    }
  }
}
```

### Setup in Startup.cs or Program.cs

```csharp
// In Program.cs or Startup.cs
services.AddFluentCmsData(Configuration.GetSection("FluentCMS:Data"));
```

This single setup method will:
1. Read the provider type from configuration
2. Register the appropriate database provider services
3. Configure the provider with the connection string and options


## Usage Examples

### Defining a Repository

```csharp
// Define the entity interface
public interface IPageRepository : IRepository<Page>
{
    Task<Page> GetBySlugAsync(string slug);
}

// Implement using EF Core
public class PageRepository : EfRepository<Page>, IPageRepository
{
    public PageRepository(EfDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Page> GetBySlugAsync(string slug)
    {
        return await FindAsync(p => p.Slug == slug);
    }
}
```

### Using the Repository and Unit of Work

```csharp
public class PageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPageRepository _pageRepository;

    public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
    {
        _unitOfWork = unitOfWork;
        _pageRepository = pageRepository;
    }

    public async Task<Page> CreatePageAsync(Page page)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _pageRepository.AddAsync(page);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return page;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

### Using Specifications

```csharp
// Define a specification
public class PublishedPageSpecification : BaseSpecification<Page>
{
    public PublishedPageSpecification()
    {
        Criteria = p => p.IsPublished;
        AddInclude(p => p.Author);
        AddOrderBy(p => p.PublishedDate);
    }
}

// Use the specification
var spec = new PublishedPageSpecification();
var publishedPages = await _pageRepository.ListAsync(spec);
```

## Database Provider Support

| Feature | Entity Framework Core | MongoDB | LiteDB |
|---------|:---------------------:|:-------:|:------:|
| CRUD Operations | ✅ | ✅ | ✅ |
| Transactions | ✅ | ✅ | ✅ |
| Complex Queries | ✅ | ✅ | ⚠️ Limited |
| Migrations | ✅ | ❌ | ❌ |
| Relationships | ✅ | ⚠️ Manual | ⚠️ Manual |
| Performance (small data) | ⚠️ | ✅ | ✅ |
| Performance (large data) | ✅ | ✅ | ⚠️ |

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Package Dependencies

Each project has minimal dependencies to ensure clean separation:

| Project | Dependencies |
|---------|--------------|
| FluentCMS.Data.Abstractions | • Microsoft.Extensions.DependencyInjection.Abstractions<br>• System.Linq.Expressions |
| FluentCMS.Data | • FluentCMS.Data.Abstractions<br>• Microsoft.Extensions.Configuration<br>• Microsoft.Extensions.DependencyInjection<br>• Microsoft.Extensions.Options |
| FluentCMS.Data.EntityFramework | • FluentCMS.Data.Abstractions<br>• Microsoft.EntityFrameworkCore<br>• Microsoft.Extensions.DependencyInjection |
| FluentCMS.Data.MongoDB | • FluentCMS.Data.Abstractions<br>• MongoDB.Driver<br>• Microsoft.Extensions.DependencyInjection |
| FluentCMS.Data.LiteDB | • FluentCMS.Data.Abstractions<br>• LiteDB<br>• Microsoft.Extensions.DependencyInjection |
