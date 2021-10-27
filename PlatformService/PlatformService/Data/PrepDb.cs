using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                try
                {

                    Console.WriteLine("--> Attempting to apply migrations..");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations : {ex.Message}");
                }
            }
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                                 new Models.Platform()
                                 {
                                     Name = ".NET",
                                     Publisher = "Microsoft",
                                     Cost = "Free"
                                 },
                                  new Models.Platform()
                                  {
                                      Name = "Sql Server Express",
                                      Publisher = "Microsoft",
                                      Cost = "Free"
                                  },
                                  new Models.Platform()
                                  {
                                      Name = "Kubernetes",
                                      Publisher = "Google",
                                      Cost = "Free"
                                  }
                 );
                context.SaveChanges();

            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
