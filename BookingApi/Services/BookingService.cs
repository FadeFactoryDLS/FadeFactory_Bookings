using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BookingAPI.Models;

namespace BookingAPI.Services;

public class BookingService
{
    private Container _container;

    public BookingService(CosmosClient dbClient, string? DatabaseID, string? ContainerID)  //DatabaseID, ContainerID
    {
 
        if (DatabaseID == null)
    {
        throw new ArgumentNullException(nameof(DatabaseID));
    }

        if (ContainerID == null)
    {
        throw new ArgumentNullException(nameof(ContainerID));
    }
       _container = dbClient.GetContainer(DatabaseID, ContainerID);
    }

    public async Task AddItemAsync(Booking item)
    {
        await _container.CreateItemAsync<Booking>(item, new PartitionKey(item.id));
    }
}