namespace FluentCMS.Data.Abstractions.Configuration;

/// <summary>
/// Base options class for database configuration
/// </summary>
public class DataOptions
{
    /// <summary>
    /// Gets or sets the database provider type
    /// </summary>
    public DataProviderType Provider { get; set; }
    
    /// <summary>
    /// Gets or sets the connection string for the database
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the optional default schema name (for relational databases)
    /// </summary>
    public string? SchemaName { get; set; }
    
    /// <summary>
    /// Gets or sets whether to automatically apply migrations on startup (where supported)
    /// </summary>
    public bool AutoMigrate { get; set; }
    
    /// <summary>
    /// Gets or sets whether to enable detailed logging (provider specific)
    /// </summary>
    public bool EnableDetailedLogging { get; set; }
}
