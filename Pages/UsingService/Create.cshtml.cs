using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShopWeb.Data;
using BarberShopWeb.Models;

namespace BarberShopWeb.Pages.UsingService
{
    public class CreateModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public CreateModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
          
            ViewData["AppointmentID"] = new SelectList(_context.Appointment, "Id", "AppointmentName");
        ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public UsingServices UsingServices { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UsingServices.Add(UsingServices);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
