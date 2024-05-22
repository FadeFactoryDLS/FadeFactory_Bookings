using BookingAPI.Models;

namespace BookingAPI.Services;

public interface IBookingService
{
    Task<Booking> GetBooking(int bookingId);
    Task<IEnumerable<Booking>> GetAllBookings();
    Task<Booking> UpdateBooking(Booking updatedBooking);
    Task<Booking> AddBooking(Booking booking);
    Task DeleteBooking(int bookingId);
}