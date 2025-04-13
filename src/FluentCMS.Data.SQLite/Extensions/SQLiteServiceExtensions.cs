using FluentCMS.Data.Abstractions;
using FluentCMS.Data.Configuration;
using FluentCMS.Data.Extensions; // Add this using directive
using FluentCMS.Data.SQLite.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentCMS.Data.SQLite.Extensions
{
    /// <summary>
    /// SQLite provider implementation of IDataProvider
    /// </summary>
    public class SQLiteProvider : IDataProvider
    {
        /// <summary>
        /// Configures services for the SQLite provider
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="options">The data options</param>
        public void ConfigureServices(IServiceCollection services, FluentCmsDataOptions options)
        {
            // Register DbContext
            services.AddDbContext<SQLiteDbContext>(dbOptions =>
            {
                dbOptions.UseSqlite(options.ConnectionString);
            });

            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(SQLiteRepository<>));
        }
    }

    /// <summary>
    /// Extension methods for configuring SQLite services
    /// </summary>
    public static class SQLiteServiceExtensions
    {
        /// <summary>
        /// Adds FluentCMS data services with SQLite provider to the specified service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="connectionString">The SQLite connection string</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection AddFluentCmsSQLiteData(this IServiceCollection services, string connectionString)
        {
            services.AddFluentCmsData(options =>
            {
                options.UseProvider<SQLiteProvider>();
                options.ConnectionString = connectionString;
            });

            return services;
        }
    }
}