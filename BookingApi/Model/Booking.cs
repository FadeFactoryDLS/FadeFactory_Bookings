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

    public DateTime Datetime { get; set; }

    public bool Isbooked { get; set; }


    public Booking(string id, string email, DateTime dateTime, bool isbooked)
    {
        this.id = id;
        Email = email;
        Datetime = dateTime;
        Isbooked = isbooked;
    }

}


