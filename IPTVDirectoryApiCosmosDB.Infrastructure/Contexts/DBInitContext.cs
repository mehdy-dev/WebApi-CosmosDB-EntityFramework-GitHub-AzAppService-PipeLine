
using IPTVDirectoryApiCosmosDB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Collections.Generic;

namespace IPTVDirectoryApiCosmosDB.Infrastructure.Contexts
{
    public class DBInitContext : DbContext
    {
        // if the db already exist so this value will be false otherwise true
        public bool _db_model_changed = false;
        public DBInitContext()
        {
        }

        public DBInitContext(DbContextOptions<DBInitContext> options) : base(options)
        {
            _db_model_changed = this.Database.EnsureCreated();
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
