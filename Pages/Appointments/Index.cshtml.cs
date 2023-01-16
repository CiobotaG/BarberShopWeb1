using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Data;
using BarberShopWeb.Models;

namespace BarberShopWeb.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public IndexModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get;set; } = default!;

        public AppointmentData AppointmentD { get; set; }
        public int AppointmentID { get; set; }
        public int CategoryID { get; set; }
        public string AppointmentNameSort { get; set; }
        public string HairstylistSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string
searchString)
        {
    
        
           
                AppointmentD = new AppointmentData();
                
            


            AppointmentNameSort = String.IsNullOrEmpty(sortOrder) ? "appointmentname_desc" : "";
            HairstylistSort = String.IsNullOrEmpty(sortOrder) ? "hairstylist_desc" : "";

            CurrentFilter = searchString;

            AppointmentD.Appointments = await _context.Appointment
            .Include(b => b.Salon).Include(b => b.Hairstylists)
            .Include(b => b.AppointmentCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.AppointmentName)
            .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                AppointmentD.Appointments = AppointmentD.Appointments.Where(s => s.AppointmentName.Contains(searchString)

                
               || s.AppointmentName.Contains(searchString));

                if (id != null)
            {
                AppointmentID = id.Value;
                Appointment appointment = AppointmentD.Appointments
                .Where(i => i.Id == id.Value).Single();
                AppointmentD.Categories = appointment.AppointmentCategories.Select(s => s.Category);
            }
            switch (sortOrder)
            {
                case "appointmentname_desc":
                    AppointmentD.Appointments = AppointmentD.Appointments.OrderByDescending(s =>
                   s.AppointmentName);
                    break;
                case "hairstylist_desc":
                    AppointmentD.Appointments = AppointmentD.Appointments.OrderByDescending(s =>
                   s.Hairstylists.FullName);
                    break;
            }
            }

    }
}
    }
