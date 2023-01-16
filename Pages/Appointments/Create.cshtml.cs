using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShopWeb.Data;
using BarberShopWeb.Models;
using System.Security.Policy;
using BarberShopWeb.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Appointments
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : AppointmentCategoriesPageModel

    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public CreateModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var hairstylistList = _context.Hairstylists.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.Name
            });
            ViewData["HairstylistID"] = new SelectList(hairstylistList, "ID", "FullName");
            ViewData["SalonID"] = new SelectList(_context.Salon, "ID",
           "SalonName");

            var appointment = new Appointment();
            appointment.AppointmentCategories = new List<AppointmentCategory>();
            PopulateAssignedCategoryData(_context, appointment);
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newAppointment = new Appointment();
            if (selectedCategories != null)
            {
                newAppointment.AppointmentCategories = new List<AppointmentCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new AppointmentCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newAppointment.AppointmentCategories.Add(catToAdd);
                }
            }
            Appointment.AppointmentCategories = newAppointment.AppointmentCategories;
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        
        PopulateAssignedCategoryData(_context, newAppointment);
        return Page();
        }
    }
}
