using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookingAPI.Models;

public class Booking
{
    [Key, Required]
    [JsonProperty("bookingId")]
    public int BookingId { get; set; }

    [StringLength(255), Required]
    public string Email { get; set; }

    [Required]
    public DateTime Timeslot { get; set; }

    public override string ToString()
    {
        return $"id: {BookingId}, Email: {Email}, Timeslot: {Timeslot}";
    }
}


