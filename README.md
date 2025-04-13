# FluentCMS.Data

A modular, provider-independent database abstraction layer for FluentCMS, designed to provide a consistent data access pattern across different applications and database providers.

## Overview

This project implements the Repository pattern to create a clean separation between the data access logic and business logic of FluentCMS. The abstraction layer supports multiple database providers with SQLite via Entity Framework as the primary implementation, and additional providers (SQL Server, PostgreSQL, MongoDB, LiteDB, MySQL) that can be added as needed.

## Purpose

- **Reusability**: Ensure the data access code can be shared across different FluentCMS applications
- **Flexibility**: Support different database technologies through a common interface
- **Testability**: Make it easy to substitute real repositories with mock implementations for testing
- **Maintainability**: Centralize data access code in a single library to simplify maintenance

## Features

- **Generic Repository Pattern**: Common interface for working with data regardless of the underlying database
- **Specification Pattern**: Encapsulates query logic to make it reusable and composable
- **Pagination Support**: Built-in support for paging through large datasets
- **Asynchronous Operations**: Support for async/await patterns to improve performance in I/O-bound operations
- **Dependency Injection**: Integrates with ASP.NET Core's built-in DI container for easy configuration and usage
- **Multiple Sorting Options**: Support for sorting data based on multiple fields and directions
- **Filtering**: Built-in support for filtering data based on various criteria
- **Audit Trails**: Automatic tracking of creation and modification timestamps
- **Provider-Specific Optimizations**: Each provider implementation can leverage database-specific features while maintaining the common interface

## Dynamic Database Provider Configuration

FluentCMS.Data supports runtime selection of database providers through configuration, allowing you to switch databases without rebuilding your application.

## Getting Started

### Installation

```bash
dotnet add package FluentCMS.Data
```

For SQLite support (default provider):
```bash
dotnet add package FluentCMS.Data.SQLite
```

### Basic Usage

```csharp
// Register services in your application startup
services.AddFluentCmsData(options => {
    options.UseProvider<SQLiteProvider>();
    options.ConnectionString = "Data Source=fluentcms.db";
});

// In your application code
public class ProductService
{
    private readonly IRepository<Product> _productRepository;
    
    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> GetProductById(int id, CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetById(id, cancellationToken);
    }
    
    public async Task UpdateInventory(int productId, int quantity, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetById(productId, cancellationToken);
        product.StockQuantity = quantity;
        await _productRepository.Update(product, cancellationToken);
    }
}
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.