using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;
using System.Security.Policy;
using BarberShopWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Salons
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public IndexModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IList<Salon> Salon { get; set; } = default!;

        public SalonIndexData SalonData { get; set; }
        public int SalonID { get; set; }
        public int AppointmentID { get; set; }
        public async Task OnGetAsync(int? id, int? appointmentID)
        {
            SalonData = new SalonIndexData();
            SalonData.Salons = await _context.Salon
            .Include(i => i.Appointments)
            .ThenInclude(c => c.Hairstylists)
            .OrderBy(i => i.SalonName)
            .ToListAsync();
            if (id != null)
            {
                SalonID = id.Value;
                Salon salon = SalonData.Salons
                .Where(i => i.ID == id.Value).Single();
                SalonData.Appointments = salon.Appointments;
            }
        }
    }
}
