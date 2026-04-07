using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Entities;

namespace Q2.Pages.Customer
{
    public class DetailModel : PageModel
    {
        private readonly Prn22226sprB11Context _context;
        public Q2.Entities.Customer? Customer { get; set; } = new();
        public DetailModel(Prn22226sprB11Context context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            Customer = _context.Customers.FirstOrDefault(e => e.CustomerId == id);
            return Page();
        }
    }
}
