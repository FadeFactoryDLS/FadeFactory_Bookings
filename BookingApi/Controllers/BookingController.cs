using Microsoft.AspNetCore.Mvc;
using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;
    public BookingsController(IBookingService service)
    {
        _service = service;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Booking>>> Get()
    {
        var bookings = await _service.GetAllBookings();
        return Ok(bookings);
    }

    [HttpGet("{bookingId}")]
    public async Task<ActionResult> Get(int bookingId)
    {
        try
        {
            var booking = await _service.GetBooking(bookingId);
            return Ok(booking);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }


    }

    [HttpPost, Authorize]
    public async Task<ActionResult<Booking>> Post(Booking booking)
    {
        Booking createdBooking = await _service.AddBooking(booking);

        String host = HttpContext.Request.Host.Value;
        String uri = $"https://{host}/api/Accounts/{createdBooking.BookingId}";

        return Created(uri, createdBooking);
    }

    [HttpPut(), Authorize]
    public async Task<IActionResult> Put(Booking booking)
    {
        try
        {
            Booking updateBooking = await _service.UpdateBooking(booking);
            return Ok(updateBooking);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{bookingId}"), Authorize]
    public async Task<IActionResult> Delete(int bookingId)
    {
        try
        {
            await _service.DeleteBooking(bookingId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
