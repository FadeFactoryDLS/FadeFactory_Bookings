using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BookingAPIModels; // Assuming your Booking model is in this namespace

public class BookingService
{
    private Container _container;

    public BookingService(CosmosClient dbClient, string databaseName, string containerName)
    {
        _container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync(Booking item)
    {
        await _container.CreateItemAsync<Booking>(item, new PartitionKey(item.Uuid));
    }

    // Add other methods for interacting with the database here
}