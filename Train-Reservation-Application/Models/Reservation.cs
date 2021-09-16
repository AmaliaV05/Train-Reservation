using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Train Train { get; set; }
        public Customer Customer { get; set; }
    }
}
