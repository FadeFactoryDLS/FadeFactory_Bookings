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
    [HttpGet()]
    public ActionResult<IEnumerable<Booking>> Get()
    {
        return new List<Booking>();
    }

    [HttpGet("{id}")]
    public ActionResult<Booking> Get(string id)
    {
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> Post([FromBody] Booking booking)
    {
            await _bookingService.AddItemAsync(booking);

            return CreatedAtAction(nameof(Get), new { id = booking.id }, booking);
    }

    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] Booking booking)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return NoContent();
    }
}
