using BookStore.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BookStore.Repositories
{
    public class UnitOfWork: BookStoreContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                var builder1 = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("environmentSetting.json");
                string environment = builder1.Build().GetSection("ENVIRONMENT").GetSection("Key").Value;
                string connectionString = string.Empty;
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile($"appsettings.{environment}.json", optional: false);
                connectionString = builder.Build().GetSection("BookStorePostgreSqlConnectionString").GetSection("BookStoreAppliationDB").Value;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }

            }
        }
    }
}
