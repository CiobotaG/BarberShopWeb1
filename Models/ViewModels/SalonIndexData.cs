using System.Security.Policy;

namespace BarberShopWeb.Models.ViewModels
{
    public class SalonIndexData
    {
        public IEnumerable<Salon> Salons { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}
