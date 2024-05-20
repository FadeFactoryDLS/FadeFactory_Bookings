using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookingAPI.Models;

public class Booking
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public string id { get; set; }

    public string Email { get; set; }

    public DateTime Timeslot { get; set; }


    public Booking(string id, string email, DateTime timeslot)
    {
        this.id = id;
        Email = email;
        Timeslot = timeslot;
    }
}


