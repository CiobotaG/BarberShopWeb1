using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Xml.Linq;

namespace BarberShopWeb.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name = "Appointment name")]
        [StringLength(150, MinimumLength = 3)]
        public string AppointmentName { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public int Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of the appointment")]
        public DateTime AppoitnmentDate { get; set; }
        public string? HairstylistID { get; set; }
        public Hairstylists? Hairstylists { get; set; }
        public int? SalonID { get; set; }
        public Salon? Salon { get; set; }
        [Display(Name = "Categories name")]
        public Category? Categories { get; set; }
        public int? CategoryID { get; set; }

        public ICollection<AppointmentCategory>? AppointmentCategories { get; set; }

    }
}
