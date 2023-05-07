using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Opening time")]
        public string OpeningTime { get; set; } = "Monday-Friday: 9am-6pm, Saturday: 10am-4pm, Sunday: Closed";
    }
}
