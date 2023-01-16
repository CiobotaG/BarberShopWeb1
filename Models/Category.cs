namespace BarberShopWeb.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<AppointmentCategory>? AppointmentCategories { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
