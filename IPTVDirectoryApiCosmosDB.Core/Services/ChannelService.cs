using IPTVDirectoryApiCosmosDB.Core.Entities;
using IPTVDirectoryApiCosmosDB.Core.Interfaces.Repositories;
using IPTVDirectoryApiCosmosDB.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPTVDirectoryApiCosmosDB.Core.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _ChannelRepository;
        public ChannelService(IChannelRepository ChannelRepository) => _ChannelRepository = ChannelRepository;

        public async Task<Channel> AddChannel(Channel Channel)
        {
            return await this._ChannelRepository.AddAsync(Channel);
        }

        public async Task<IReadOnlyList<Channel>> ListAllChannels()
        {
            return await this._ChannelRepository.ListAllAsync();
        }

        public async Task<Channel> GetChannel(string ChannelId)
        {
            Guid.TryParse(ChannelId, out Guid chGuid);
            return await this._ChannelRepository.GetByIdAsync(chGuid);
        }

        public async Task UpdateChannel(Channel Channel)
        {
            await this._ChannelRepository.UpdateAsync(Channel);
        }

        public async Task DeleteChannel(string ChannelId)
        {
            Guid.TryParse(ChannelId, out Guid chGuid);
            var Channel = await this._ChannelRepository.GetByIdAsync(chGuid);
            await this._ChannelRepository.DeleteAsync(Channel);
        }
    }
}
