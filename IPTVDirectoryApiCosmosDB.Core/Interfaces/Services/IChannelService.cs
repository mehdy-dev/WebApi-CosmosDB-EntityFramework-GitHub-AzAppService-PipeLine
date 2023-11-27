using IPTVDirectoryApiCosmosDB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPTVDirectoryApiCosmosDB.Core.Interfaces.Services
{
    public interface IChannelService
    {
        Task<Channel> GetChannel(string ChannelId);
        Task<Channel> AddChannel(Channel Channel);
        Task<IReadOnlyList<Channel>> ListAllChannels();
        Task DeleteChannel(string ChannelId);
        Task UpdateChannel(Channel Channel);
    }
}
