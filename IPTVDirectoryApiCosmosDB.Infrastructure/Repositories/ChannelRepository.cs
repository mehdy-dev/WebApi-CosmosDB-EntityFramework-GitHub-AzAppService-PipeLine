
using IPTVDirectoryApiCosmosDB.Core.Entities;
using IPTVDirectoryApiCosmosDB.Core.Interfaces.Repositories;
using IPTVDirectoryApiCosmosDB.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IPTVDirectoryApiCosmosDB.Infrastructure.Repositories
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ChannelContext _context;

        public ChannelRepository(ChannelContext ChannelContext) => _context = ChannelContext;

        public async Task<Channel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //// var keyValues = new object[] { id };
            ////##  check the db context for diffferent session  
            //// Get the type of the object
            //int objectId = _context.GetHashCode();
            ////  Console.WriteLine($"Object Identifier DBInitContext : {objectId}");
            //Console.WriteLine($"Object Identifier of ChannelDbContext ChannelRepository : {_context.ContextId}");
            ////## check the db context for diffferent session 

           // var Channel =  await this._context.Set<Channel>().FindAsync(id, cancellationToken);
            var Channel = await this._context.Set<Channel>().FindAsync(id);

            Channel ch;

            // Guid.TryParse(Channel.Id.ToString(), out Guid chGuid);
            if (Channel != null)
            {
                 ch = new Channel
                {
                    id = id,
                    tv_id = Channel.tv_id,
                    tv_name = Channel.tv_name,
                    url = Channel.url,
                    logo = Channel.logo,
                    category = Channel.category,
                    country = Channel.country
                };
            }
            else
            {
                ch = new Channel
                {
                    id = id,
                    tv_id = "not found",
                    tv_name = "not found",
                    url = "not found",
                    logo = "not found",
                    category = "not found",
                    country = "not found"
                };
            }
            return ch;
        }
        public async Task<Channel> AddAsync(Channel entity, CancellationToken cancellationToken = default)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Channel entity, CancellationToken cancellationToken = default)
        {
            this._context.Remove(entity);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Channel entity, CancellationToken cancellationToken = default)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Channel>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var Channels = await this._context.Channels.ToListAsync();

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;

            return Channels;
        }
    }
}
