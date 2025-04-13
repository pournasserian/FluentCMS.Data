using FluentCMS.Data.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentCMS.Data.Extensions;

/// <summary>
/// Extension methods for setting up FluentCMS.Data services in an <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds FluentCMS data services to the specified <see cref="IServiceCollection" />
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configureOptions">The action to configure options</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddFluentCmsData(this IServiceCollection services, Action<FluentCmsDataOptions> configureOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        // Create options with a default empty connection string to satisfy the required property
        var options = new FluentCmsDataOptions { ConnectionString = string.Empty };
        configureOptions(options);

        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            throw new InvalidOperationException("Connection string must be provided.");
        }

        if (options.ProviderType == null)
        {
            throw new InvalidOperationException("No data provider was configured. Call UseProvider<TProvider>() in options configuration.");
        }

        // Create an instance of the provider
        var providerInstance = Activator.CreateInstance(options.ProviderType);
        
        if (providerInstance is IDataProvider provider)
        {
            // Configure the provider services
            provider.ConfigureServices(services, options);
        }
        else
        {
            throw new InvalidOperationException($"The type {options.ProviderType.FullName} does not implement IDataProvider.");
        }

        return services;
    }
}