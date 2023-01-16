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
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BarberShopWeb.Pages.Appointments
{
    [Authorize(Roles = "Admin")]
    public class EditModel : AppointmentCategoriesPageModel
    {
        private readonly BarberShopWeb.Data.BarberShopWebContext _context;

        public EditModel(BarberShopWeb.Data.BarberShopWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointment
 .Include(b => b.Salon)
 .Include(b => b.AppointmentCategories).ThenInclude(b => b.Category)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.Id == id);

            if (Appointment == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Appointment);

            var hairstylistList = _context.Hairstylists.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.Name
            });
            ViewData["HairstylistID"] = new SelectList(hairstylistList, "ID", "FullName");
            ViewData["SalonID"] = new SelectList(_context.Set<Salon>(), "ID", "SalonName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var appointmentToUpdate = await _context.Appointment
            .Include(i => i.Salon)
            .Include(i => i.AppointmentCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.Id == id);
            if (appointmentToUpdate == null)
            {
                return NotFound();
            }
            //se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Appointment>(
            appointmentToUpdate,
            "Appointment",
            i => i.AppointmentName, i => i.Hairstylists,
            i => i.Price, i => i.AppoitnmentDate, i => i.SalonID))
            {
                UpdateBookCategories(_context, selectedCategories, appointmentToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdateBookCategories(_context, selectedCategories, appointmentToUpdate);
            PopulateAssignedCategoryData(_context, appointmentToUpdate);
            return Page();
        }



        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }

    }
}
