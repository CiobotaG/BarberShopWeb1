using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Salons
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public DetailsModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

      public Salon Salon { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Salon == null)
            {
                return NotFound();
            }

            var salon = await _context.Salon.FirstOrDefaultAsync(m => m.ID == id);
            if (salon == null)
            {
                return NotFound();
            }
            else 
            {
                Salon = salon;
            }
            return Page();
        }
    }
}
