using Microsoft.Extensions.DependencyInjection;

namespace FluentCMS.Data.Configuration
{
    /// <summary>
    /// Interface for database providers
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Configures services for the provider
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="options">The data options</param>
        void ConfigureServices(IServiceCollection services, FluentCmsDataOptions options);
    }
}