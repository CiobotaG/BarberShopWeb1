using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShopWeb.Models
{
    public class Hairstylists
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }

        public ICollection<Appointment>? Appointments { get; set; }

    }
}
