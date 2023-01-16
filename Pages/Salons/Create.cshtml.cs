using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShopWeb.Data;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Salons
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public CreateModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Salon Salon { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Salon.Add(Salon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
