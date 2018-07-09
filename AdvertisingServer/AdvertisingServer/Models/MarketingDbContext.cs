using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingServer.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingServer.Models
{
    public class MarketingDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MarketingDbContext(DbContextOptions<MarketingDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseInMemoryDatabase("MarketingDb");
            }
        }

        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Publishing> Publishings { get; set; }
    }
}
