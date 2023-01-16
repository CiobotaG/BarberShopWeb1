using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace BarberShopWeb.Models
{
    public class UsingServices
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
        public int? AppointmentID { get; set; }
        public Appointment? Appointment { get; set; }
        [DataType(DataType.Date)]
        public DateTime ConfirmationDate { get; set; }
    }
}
