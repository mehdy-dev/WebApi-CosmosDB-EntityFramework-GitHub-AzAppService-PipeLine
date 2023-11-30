using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using IPTVDirectoryApiCosmosDB.Core.Entities;
using IPTVDirectoryApiCosmosDB.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Cosmos.Metadata;

namespace IPTVDirectoryApiCosmosDB.Infrastructure.Initialization
{
    public class CosmosInitializer : IHostedService
    {

        private readonly IServiceScopeFactory scopeFactory;

        public CosmosInitializer(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }


        public  async Task DoWork()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DBInitContext>();

                //  //##  check the db context for Host Service 
                //  // Get the type of the object
                //  int objectId = dbContext.GetHashCode();
                ////  Console.WriteLine($"Object Identifier DBInitContext : {objectId}");
                //  Console.WriteLine($"Object Identifier of DBInitContext : {dbContext.ContextId}");
                //  //## check the db context for Host Service 

                // check the model and if already exist ignore initialization 

                // we can use in the case of model change 
                //ensure it is compatible with the model for this context.
                if (dbContext._db_model_changed)
                {
                    // start initialization of collections and seed items

                    Task<List<JsonChannel>> TResult = ReadJsonFile();
                    List<JsonChannel> result = await TResult;

                    await dbContext.AddRangeAsync(MapperToChannel(result));
                    await dbContext.SaveChangesAsync();

                    // end of initialization of collections and seed items
                }else { }
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialization logic goes here
            Console.WriteLine("### Application Initialization started. ###");
            await DoWork();
            Console.WriteLine("### Application Initialization finished. ###");

            // return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task<List<JsonChannel>>  ReadJsonFile()
        {

            // Specify the path to your JSON file
            string jsonFilePath = "channels.json";

            // Read and deserialize JSON content
            List<JsonChannel> channels = new List<JsonChannel>();
            channels =  ReadJsonFile<List<JsonChannel>>(jsonFilePath);

            //// Display the deserialized data
            //foreach (var ch in channels)
            //{
            //    Console.WriteLine($"Id: {ch.tv_name}, Name: {ch.tv_name}");
            //}

            return channels;
        }


        public  T ReadJsonFile<T>(string filePath)
        {
            try
            {
                // Read the entire JSON file
                string jsonString = File.ReadAllText(filePath);

                // Deserialize the JSON string to the specified type
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return default; // Return default value for the type (null for reference types)
            }
        }

        private List<Channel> MapperToChannel(List<JsonChannel> jsonChannelList)
        {
            List<Channel> channels = new List<Channel>();
            foreach(var chJ in jsonChannelList)
            {
                Guid guid;
                do { guid = Guid.NewGuid(); }
                while (channels.Any(c => c.id == guid));


                Channel channel = new Channel {
                    id = guid,
                    tv_id = chJ.tv_id,
                    tv_name = chJ.tv_name,
                    url = chJ.url,
                    logo = chJ.logo,
                    category= chJ.category,
                    country= chJ.country
                };
                channels.Add(channel);

            }
            return channels;

        }

    }
}
