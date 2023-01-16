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
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Appointments
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public DeleteModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
       
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment != null)
            {
                Appointment = appointment;
                _context.Appointment.Remove(Appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
