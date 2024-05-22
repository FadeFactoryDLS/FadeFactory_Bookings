using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Models;

public class BookingDbContext : DbContext
{
    public BookingDbContext() { }
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }
    public virtual DbSet<Booking> Bookings { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer("Bookings");
        builder.Entity<Booking>().HasNoDiscriminator();
        builder.Entity<Booking>().ToContainer("Bookings");
    }
}
