using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Managers;

public class BookingDbManager : IBookingService
{
    private readonly BookingDbContext _context;

    public BookingDbManager(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> GetBooking(int bookingId)
    {
        if (_context.Bookings == null) throw new NullReferenceException();
        var result = await _context.Bookings.FirstOrDefaultAsync(a => a.BookingId == bookingId);
        if (result == null) throw new Exception("Account not found.");
        return result;
    }

    public async Task<IEnumerable<Booking>> GetAllBookings()
    {
        var result = await _context.Bookings.ToListAsync();
        return result;
    }

    public async Task<Booking> UpdateBooking(Booking booking)
    {
        if (_context.Bookings.Count(a => a.Email == booking.Email && a.BookingId != booking.BookingId) > 0)
        {
            throw new Exception("Booking with this email already exists.");
        }

        var result = _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Booking> AddBooking(Booking booking)
    {
        int dbSize = (await _context.Bookings.ToListAsync()).LastOrDefault()?.BookingId ?? 0;
        booking.BookingId = dbSize + 1;

        if (_context.Bookings.Count(a => a.Email == booking.Email) > 0) throw new Exception("Booking with this email already exists.");

        var result = _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    public async Task DeleteBooking(int bookingId)
    {
        _context.Bookings.Remove(new Booking { BookingId = bookingId });
        await _context.SaveChangesAsync();
    }
}