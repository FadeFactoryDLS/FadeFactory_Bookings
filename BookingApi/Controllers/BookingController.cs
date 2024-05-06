using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;

    public BookingsController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }
        // GET: api/bookings
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> Get()
        {
            // Logik for at hente alle bookinger fra databasen
            // og returnere dem som en liste af Booking-objekter
            return new List<Booking>();
        }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> Get(int id)
        {
            // Logik for at hente en specifik booking fra databasen
            // baseret pÃ¥ id og returnere den som et Booking-objekt
            return NotFound(); // Returnerer NotFound, hvis bookingen ikke findes
        }


//         [HttpPost]
//         public async Task<IActionResult> CreateItem(Booking item)
// {
//          try
//         {
//    //         await _cosmosDbService.AddItemAsync(item);
//         return Ok();
//         }
//         catch (Exception ex)
//         {
//         return StatusCode(StatusCodes.Status500InternalServerError, $"Error inserting item: {ex.Message}");
//         }
// }

        // // POST: api/bookings
        // [HttpPost]

        // public ActionResult<Booking> Post([FromBody] Booking booking)
        // {
        //     // Logik for at oprette en ny booking og gemme den i databasen
        //     // Returnerer oprettet booking med HTTP-status 201 Created
        //     return CreatedAtAction(nameof(Get), new { id = booking.Uuid }, booking);
        // }

        // POST: api/bookings
        [HttpPost ("{create booking}")]
public async Task<ActionResult<Booking>> Post([FromBody] BookingAPIModels.Booking booking)
{
    try
    {
        // Logic to create a new booking and save it in the database
        await _bookingService.AddItemAsync(booking);

        // Returns created booking with HTTP-status 201 Created
        return CreatedAtAction(nameof(Get), new { id = booking.Uuid }, booking);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Error inserting item: {ex.Message}");
    }
}



        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Booking booking)
        {
            // Logik for at opdatere en eksisterende booking i databasen
            return NoContent(); // Returnerer NoContent, hvis opdateringen lykkes
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Logik for at slette en eksisterende booking fra databasen
            return NoContent(); // Returnerer NoContent, hvis sletningen lykkes
        }
    }

    public class Booking
    {
        public int Uuid { get; set; }
        public string? Email { get; set; }
        public DateTime DateTime { get; set; }
        public bool Isbooked { get; set; }
        
        // Andre relevante egenskaber for en booking
    }
}