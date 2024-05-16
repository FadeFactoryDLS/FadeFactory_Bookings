using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using BookingAPI.Models;
using BookingAPI.Services;
using DotNetEnv;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly CosmosClient _cosmosClient;

    public BookingsController(BookingService bookingService, CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
        _bookingService = bookingService;
    }
    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<IEnumerable<Booking>>> Get()
    {
        var bookings = await _bookingService.GetAllItemsAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(string id)
    {

        var booking = await _bookingService.GetItemAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> Post([FromBody] Booking booking)
    {
        await _bookingService.AddItemAsync(booking);

        return CreatedAtAction(nameof(Get), new { id = booking.id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Booking updatedBooking)
    {
        var booking = await _bookingService.GetItemAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        updatedBooking.id = id; // Ensure the id of the updated booking is the same as the id in the route
        await _bookingService.UpdateItemAsync(updatedBooking);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var booking = await _bookingService.GetItemAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        await _bookingService.DeleteItemAsync(booking);
        return NoContent();
    }


}
