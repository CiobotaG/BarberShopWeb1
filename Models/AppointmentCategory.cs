namespace BarberShopWeb.Models
{
    public class AppointmentCategory
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
