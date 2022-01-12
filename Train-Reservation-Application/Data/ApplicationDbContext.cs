using Microsoft.EntityFrameworkCore;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasMany(x => x.Calendars)
                .WithMany(x => x.Seats)
                .UsingEntity<SeatCalendar>(
                    x => x.HasOne(x => x.Calendar).WithMany(s => s.SeatCalendars),
                    x => x.HasOne(x => x.Seat).WithMany(s => s.SeatCalendars));
            modelBuilder.Entity<Reservation>()
                .HasMany(x => x.Seats)
                .WithMany(x => x.Reservations)
                .UsingEntity<ReservationSeat>(
                    x => x.HasOne(x => x.Seat).WithMany(s => s.ReservationSeats),
                    x => x.HasOne(x => x.Reservation).WithMany(r => r.ReservationSeats));
        }
    }
}
