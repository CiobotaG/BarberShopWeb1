using Microsoft.AspNetCore.Mvc.RazorPages;
using BarberShopWeb.Data;
namespace BarberShopWeb.Models
{
    public class AppointmentCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(BarberShopWebContext context,
        Appointment appointment)
        {
            var allCategories = context.Category;
            var appointmentCategories = new HashSet<int>(
            appointment.AppointmentCategories.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = appointmentCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateBookCategories(BarberShopWebContext context,
        string[] selectedCategories, Appointment appointmentToUpdate)
        {
            if (selectedCategories == null)
            {
                appointmentToUpdate.AppointmentCategories = new List<AppointmentCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var appointmentCategories = new HashSet<int>
            (appointmentToUpdate.AppointmentCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!appointmentCategories.Contains(cat.ID))
                    {
                        appointmentToUpdate.AppointmentCategories.Add(
                        new AppointmentCategory
                        {
                            AppointmentID = appointmentToUpdate.Id,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (appointmentCategories.Contains(cat.ID))
                    {
                        AppointmentCategory courseToRemove
                        = appointmentToUpdate
                        .AppointmentCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
