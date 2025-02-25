using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrimaryPixels.Data;
using PrimaryPixels;

namespace PrimaryPixelsIntegrationTest
{
    public class PrimaryPixelsWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IServiceScope Scope { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Get the previous DbContextOptions registrations 
                var primaryPixelsDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PrimaryPixelsContext>));
                var usersDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersContext>));
                if (usersDbContextDescriptor == null) throw new Exception("Couldn't find registered UserDB");
                if (primaryPixelsDbContextDescriptor == null) throw new Exception("Couldn't find registered SolarDB");
                //Remove the previous DbContextOptions registrations
                if (primaryPixelsDbContextDescriptor != null)
                    services.Remove(primaryPixelsDbContextDescriptor);

                if (usersDbContextDescriptor != null)
                    services.Remove(usersDbContextDescriptor);

                // Remove any database-related services that might be registered by other providers
                var dbProviderServices = services.Where(service => service.ImplementationType?.FullName?.Contains("SqlServer") == true).ToList();
                foreach (var service in dbProviderServices)
                {
                    services.Remove(service);
                }

                //Add new DbContextOptions for our two contexts, this time with inmemory db 
                services.AddDbContext<PrimaryPixelsContext>(options =>
                {
                    options.UseInMemoryDatabase("PrimaryPixelsDB");
                });

                services.AddDbContext<UsersContext>(options =>
                {
                    options.UseInMemoryDatabase("UsersDB");
                });

                //We will need to initialize our in memory databases. 
                //Since DbContexts are scoped services, we create a scope
                using var scope = services.BuildServiceProvider().CreateScope();

                //We use this scope to request the registered dbcontexts, and initialize the schemas
                var primaryPixelsContext = scope.ServiceProvider.GetRequiredService<PrimaryPixelsContext>();
                primaryPixelsContext.Database.EnsureDeleted();
                primaryPixelsContext.Database.EnsureCreated();

                var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
                userContext.Database.EnsureDeleted();
                userContext.Database.EnsureCreated();

                //Here we could do more initializing if we wished (e.g. adding admin user)
            });
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);
            Scope = host.Services.CreateScope();
            return host;
        }
    }
}
