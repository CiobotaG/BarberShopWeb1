using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberShopWeb.Pages.Appointments
{
    public class DetailsModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public DetailsModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

      public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["HairstylistID"] = new SelectList(_context.Hairstylists, "ID", "Name");
            ViewData["SalonID"] = new SelectList(_context.Salon, "ID",
           "SalonName");
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            else 
            {
                Appointment = appointment;
            }
            return Page();
        }
    }
}
