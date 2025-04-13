using System;

namespace FluentCMS.Data.Configuration
{
    /// <summary>
    /// Options for configuring FluentCMS.Data
    /// </summary>
    public class FluentCmsDataOptions
    {
        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the data provider type
        /// </summary>
        public Type? ProviderType { get; private set; }

        /// <summary>
        /// Configures the data access layer to use the specified provider
        /// </summary>
        /// <typeparam name="TProvider">The type of provider to use</typeparam>
        public void UseProvider<TProvider>() where TProvider : class, IDataProvider
        {
            ProviderType = typeof(TProvider);
        }
    }
}