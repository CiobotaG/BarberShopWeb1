using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;

namespace BarberShopWeb.Pages.UsingService
{
    public class EditModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public EditModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UsingServices UsingServices { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UsingServices == null)
            {
                return NotFound();
            }

            var usingservices =  await _context.UsingServices.FirstOrDefaultAsync(m => m.ID == id);
            if (usingservices == null)
            {
                return NotFound();
            }
            UsingServices = usingservices;
           ViewData["AppointmentID"] = new SelectList(_context.Appointment, "Id", "AppointmentName");
           ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UsingServices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsingServicesExists(UsingServices.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsingServicesExists(int id)
        {
          return _context.UsingServices.Any(e => e.ID == id);
        }
    }
}
