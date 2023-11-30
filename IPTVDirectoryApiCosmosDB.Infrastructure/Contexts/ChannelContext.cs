
using IPTVDirectoryApiCosmosDB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace IPTVDirectoryApiCosmosDB.Infrastructure.Contexts
{
    public class ChannelContext: DbContext
    {
        public ChannelContext()
        { 
        }

        public ChannelContext(DbContextOptions<ChannelContext> options): base(options)
        {
           this.Database.EnsureCreated();
        }

        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>()
                .ToContainer("Channels")
                .HasPartitionKey(c => c.country)
                .HasNoDiscriminator();

            modelBuilder.Entity<Channel>()
                .Property(c => c.id).HasConversion<string>();


        }
    }
}
