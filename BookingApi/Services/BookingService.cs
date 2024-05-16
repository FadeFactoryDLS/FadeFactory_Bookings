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

    public async Task<Booking> GetItemAsync(string id)
    {
        return (await _container.ReadItemAsync<Booking>(id, new PartitionKey(id))).Resource;
    }

    public async Task<IEnumerable<Booking>> GetAllItemsAsync()
    {
        var query = _container.GetItemQueryIterator<Booking>(new QueryDefinition("SELECT * FROM c"));
        List<Booking> results = new List<Booking>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task UpdateItemAsync(Booking updatedBooking)
    {
        await _container.ReplaceItemAsync(updatedBooking, updatedBooking.id, new PartitionKey(updatedBooking.id));
    }

    public async Task AddItemAsync(Booking item)
    {
        await _container.CreateItemAsync<Booking>(item, new PartitionKey(item.id));
    }
    public async Task DeleteItemAsync(Booking item)
    {
        await _container.DeleteItemAsync<Booking>(item.id, new PartitionKey(item.id));
    }



}