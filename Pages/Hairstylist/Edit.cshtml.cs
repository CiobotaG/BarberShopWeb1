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

namespace BarberShopWeb.Pages.Hairstylist
{
    public class EditModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public EditModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Hairstylists Hairstylists { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Hairstylists == null)
            {
                return NotFound();
            }

            var hairstylists =  await _context.Hairstylists.FirstOrDefaultAsync(m => m.ID == id);
            if (hairstylists == null)
            {
                return NotFound();
            }
            Hairstylists = hairstylists;
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

            _context.Attach(Hairstylists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HairstylistsExists(Hairstylists.ID))
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

        private bool HairstylistsExists(int id)
        {
          return _context.Hairstylists.Any(e => e.ID == id);
        }
    }
}
