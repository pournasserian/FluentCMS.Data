# FluentCMS.Data

A modular, provider-independent database abstraction layer for FluentCMS, designed to provide a consistent data access pattern across different applications and database providers.

## Overview

This project implements the Repository and Unit of Work patterns to create a clean separation between the data access logic and business logic of FluentCMS. The abstraction layer supports multiple database providers (SQL Server, PostgreSQL, SQLite, MongoDB, LiteDB, MySQL) and can be extended to support additional providers as needed.

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
- **Transaction Support**: Ensures data consistency across multiple operations
- **Provider-Specific Optimizations**: Each provider implementation can leverage database-specific features while maintaining the common interface


## Dynamic Database Provider Configuration

FluentCMS.Data supports runtime selection of database providers through configuration, allowing you to switch databases without rebuilding your application.