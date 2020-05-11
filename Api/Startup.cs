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

            var connectionString = GetConnectionString();
            
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

        public string GetConnectionString()
        { 
            var connectionString = Configuration.GetConnectionString("ViceConnectionString");

            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            }
            else
            {
                Console.WriteLine("Using connection string from ViceConnectionString from appsettings");
            }

            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("You must specify a valid connection string either as an environment variable name ConnectionString or as an AppSetting named ConnectionString.");
            }
            else
            {
                Console.WriteLine("Using connection string from ConnectionString from Environment Variable");
            }
            
            Console.WriteLine("Connecting to database: " + connectionString);

            return connectionString;
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
