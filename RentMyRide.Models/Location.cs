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
        [DisplayName("First name")]
        public string PhoneNumber { get; set; }
        [DisplayName("Opening time")]
        public string OpeningTime { get; set; }
    }
}
