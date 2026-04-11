using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Entities;

namespace Q2.Pages.Customer
{
    public class ListModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper1Context _context;
        public List<Q2.Entities.Customer> Customers { get; set; } = new();
        public List<string> Cities { get; set; } = new();
        public string SelectedValue { get; set; } = String.Empty;
        public ListModel(PRN222_TestQuestion_Paper1Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Customers = _context.Customers.ToList();
            Cities = _context.Customers.Select(c => c.City).Distinct().ToList();
        }
    }
}
