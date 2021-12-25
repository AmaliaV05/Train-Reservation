using System.Collections.Generic;

namespace Train_Reservation_Application.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
