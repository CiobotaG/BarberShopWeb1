using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;

namespace BarberShopWeb.Pages.UsingService
{
    public class IndexModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public IndexModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IList<UsingServices> UsingServices { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UsingServices != null)
            {
                UsingServices = await _context.UsingServices
                .Include(u => u.Appointment)
                .Include(u => u.Client).ToListAsync();
            }
        }
    }
}
