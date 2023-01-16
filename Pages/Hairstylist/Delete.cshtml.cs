using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;

namespace BarberShopWeb.Pages.Hairstylist
{
    public class DeleteModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public DeleteModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Hairstylists Hairstylists { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Hairstylists == null)
            {
                return NotFound();
            }

            var hairstylists = await _context.Hairstylists.FirstOrDefaultAsync(m => m.ID == id);

            if (hairstylists == null)
            {
                return NotFound();
            }
            else 
            {
                Hairstylists = hairstylists;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Hairstylists == null)
            {
                return NotFound();
            }
            var hairstylists = await _context.Hairstylists.FindAsync(id);

            if (hairstylists != null)
            {
                Hairstylists = hairstylists;
                _context.Hairstylists.Remove(Hairstylists);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
