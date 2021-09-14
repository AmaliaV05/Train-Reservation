using Microsoft.EntityFrameworkCore;

namespace Train_Reservation_Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        {
        }
    }
}
