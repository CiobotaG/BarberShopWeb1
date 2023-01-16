namespace BarberShopWeb.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
