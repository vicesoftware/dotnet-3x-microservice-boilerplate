using System;
using Api.Domain;
using Api.Domain.Documents;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Api
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            var connectionString = Configuration.GetConnectionString("ViceConnectionString");

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("WARNING: Unable to read connection string ViceConnectionString from appsettings");
                connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            }

            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=localhost;Database=test-database;User Id=sa;Password=Top-Secret";
                Console.WriteLine("WARNING: Unable to read connection string Environment Variable from appsettings");
                // throw new Exception("You must specify a valid connection string either as an environment variable name ConnectionString or as an AppSetting named ConnectionString.");
            }
            
            Console.WriteLine("Connecting to database: " + connectionString);
            
            services.AddDbContext<ViceContext>(options =>
                options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ViceContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
#if DEBUG
            DatabaseInitializer.Initialize(context);
#endif
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
    public static class DatabaseInitializer
    {
        public static void Initialize(ViceContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            if (!dbContext.Documents.Any())
            {
                // Write you necessary to code here to insert the User to database and save the the information to file.
                Console.WriteLine("Initializing database with dummy data...");
                dbContext.Documents.Add(new Document
                {
                    Id = Guid.NewGuid(),
                    Name = "FOO"
                });
                dbContext.Documents.Add(new Document
                {
                    Id = Guid.NewGuid(),
                    Name = "Bar"
                });
                
                dbContext.SaveChanges();
            }

        }
    }
}
