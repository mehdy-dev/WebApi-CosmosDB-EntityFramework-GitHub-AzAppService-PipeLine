
using IPTVDirectoryApiCosmosDB.Core.Interfaces.Repositories;
using IPTVDirectoryApiCosmosDB.Infrastructure.Contexts;
using IPTVDirectoryApiCosmosDB.Infrastructure.Initialization;
using IPTVDirectoryApiCosmosDB.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IPTVDirectoryApiCosmosDB.Infrastructure.Extentions
{
	public static class DependencyInjection
	{
		public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ChannelContext>(options =>
				options.UseCosmos(
					configuration["Cosmos:AccountEndpoint"],
					configuration["Cosmos:AccountKey"],
					configuration["Cosmos:DatabaseName"])
			);
			services
				.AddScoped<IChannelRepository, ChannelRepository>();


		}
	}
}
