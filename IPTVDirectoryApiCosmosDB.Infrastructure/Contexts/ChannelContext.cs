
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
             /*
             it is necessary to use conversion otherwise we cannot add it to db set when trying to save the cosmo db 
             context will reject saving and says it should be string value
             AutoGenerator is not mandatory as cosmodb will generate it automatically 
             */
            modelBuilder.Entity<Channel>()
                .Property(c => c.id)
                .HasConversion<string>();
            //    .HasValueGenerator<SequentialGuidValueGenerator>();
        }
    }
}
