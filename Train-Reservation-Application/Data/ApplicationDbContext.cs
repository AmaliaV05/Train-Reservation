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
    }
}
