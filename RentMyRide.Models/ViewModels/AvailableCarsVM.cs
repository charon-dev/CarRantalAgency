using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace RentMyRide.Models.ViewModels
{
    public class AvailableCarsVM
    {
       public IEnumerable<Car> cars { get; set; }
        public IEnumerable<string> MakesList { get; set; }
        public IEnumerable<string> ColorsList { get; set; }
        public double Price { get; set; }
        public int Places { get; set; }
        public IEnumerable<string> GearboxesList { get; set; }
        public IEnumerable<Location> LocationsList { get; set; }
        public IPagedList<Car> PagedCars { get; set; }
    }
}
