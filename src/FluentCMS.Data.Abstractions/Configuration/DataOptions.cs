namespace FluentCMS.Data.Abstractions.Configuration;

public class DataOptions
{
    public DataProviderType Provider { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public Dictionary<string, object>? Options { get; set; }
}
