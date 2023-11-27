using IPTVDirectoryApiCosmosDB.Core.Interfaces.Services;
using IPTVDirectoryApiCosmosDB.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IPTVDirectoryApiCosmosDB.Core.Extensions
{
    public static class DependencyInjection
    {
		public static void AddCoreServices(this IServiceCollection services)
		{
			services
				.AddScoped<IChannelService, ChannelService>();
		}
	}
}
