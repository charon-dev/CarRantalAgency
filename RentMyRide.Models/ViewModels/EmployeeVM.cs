using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models.ViewModels
{
    public class EmployeeVM
    {
        public Employee employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }
    }
}
