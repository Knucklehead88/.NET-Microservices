using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext context;

        public PlatformRepo(AppDbContext context)
        {
            this.context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            context.Platforms.Add(platform);
        }

        public System.Collections.Generic.IEnumerable<Platform> GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }
        public Platform GetPlaformById(int id)
        {
            return context.Platforms.SingleOrDefault(prop => prop.Id == id);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}