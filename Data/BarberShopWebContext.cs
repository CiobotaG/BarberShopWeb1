using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShopWeb.Data
{
    public class BarberShopWebContext : DbContext
    {
       
        public BarberShopWebContext (DbContextOptions<BarberShopWebContext> options)
            : base(options)
        {
        }

        public DbSet<BarberShopWeb.Models.Appointment> Appointment { get; set; } = default!;

        public DbSet<BarberShopWeb.Models.Salon> Salon { get; set; }

        public DbSet<BarberShopWeb.Models.Hairstylists> Hairstylists { get; set; }

        public DbSet<BarberShopWeb.Models.Category> Category { get; set; }

        public DbSet<BarberShopWeb.Models.Client> Client { get; set; }

        public DbSet<BarberShopWeb.Models.UsingServices> UsingServices { get; set; }
    }
}
