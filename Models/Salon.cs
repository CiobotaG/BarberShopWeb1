using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShopWeb.Models
{
    public class Salon
    {
        public int ID { get; set; }
        [Display(Name = "Salon name")]
        public string SalonName { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
