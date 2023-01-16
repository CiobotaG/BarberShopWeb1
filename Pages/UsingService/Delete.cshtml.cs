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

namespace BarberShopWeb.Pages.UsingService
{
    public class DeleteModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public DeleteModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UsingServices UsingServices { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["AppointmentID"] = new SelectList(_context.Appointment, "Id", "AppointmentName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");

            if (id == null || _context.UsingServices == null)
            {
                return NotFound();
            }

            var usingservices = await _context.UsingServices.FirstOrDefaultAsync(m => m.ID == id);

            if (usingservices == null)
            {
                return NotFound();
            }
            else 
            {
                UsingServices = usingservices;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UsingServices == null)
            {
                return NotFound();
            }
            var usingservices = await _context.UsingServices.FindAsync(id);

            if (usingservices != null)
            {
                UsingServices = usingservices;
                _context.UsingServices.Remove(UsingServices);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
