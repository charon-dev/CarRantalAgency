using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentMyRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.DataAcess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Renting> Renting { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Reservation_Services> Reservation_Services { get; set; }
        public DbSet<Renting_Services> Renting_Services { get; set; }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



    }
}
