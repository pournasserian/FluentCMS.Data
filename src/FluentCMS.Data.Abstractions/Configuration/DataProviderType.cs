namespace FluentCMS.Data.Abstractions.Configuration;

/// <summary>
/// Enumeration of supported database provider types
/// </summary>
public enum DataProviderType
{
    /// <summary>
    /// Microsoft Entity Framework Core provider (supports SQL Server, PostgreSQL, SQLite, etc.)
    /// </summary>
    EntityFramework,
    
    /// <summary>
    /// MongoDB provider for NoSQL document database
    /// </summary>
    MongoDB,
    
    /// <summary>
    /// LiteDB provider for embedded NoSQL document database
    /// </summary>
    LiteDB
}
