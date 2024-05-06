using System;
using System.ComponentModel.DataAnnotations;

namespace BookingAPIModels
{
        public class Booking
        {
            [Key]
            public int Uuid {get; set;}

            public string Email {get; set;}

            public DateTime Datetime {get; set;}

            public bool Isbooked {get; set;}

            public Booking()
            {

            }

        public Booking(int uuid, string email, DateTime dateTime, bool isbooked)
        {
            Uuid = uuid;
            Email = email;
            Datetime = dateTime;
            Isbooked = isbooked;
        }

    }
}

