using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GestiStage.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GestiStage
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var sp = host.Services.GetService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider;
            var options = sp.GetRequiredService<DbContextOptions<GestiStageDbContext>>();
            await EnsureDbCreatedAndSeedAsync(options);
            // back to your regularly scheduled program

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// Method to see the database. Should not be used in production: demo purposes only.
        /// </summary>
        /// <param name="options">The configured options.</param>
        /// <param name="count">The number of contacts to seed.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private static async Task EnsureDbCreatedAndSeedAsync(DbContextOptions<GestiStageDbContext> options)
        {
            // empty to avoid logging while inserting (otherwise will flood console)
            var factory = new LoggerFactory();
            var builder = new DbContextOptionsBuilder<GestiStageDbContext>(options)
                .UseLoggerFactory(factory);

            using var context = new GestiStageDbContext(builder.Options);
            // result is true if the database had to be created
            if (await context.Database.EnsureCreatedAsync())
            {
                var seed = new GestiStageDbInit();
                await seed.SeedDB(context);
            }
        }
    }
}
