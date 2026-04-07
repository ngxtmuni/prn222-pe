using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Entities;

namespace Q2.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly Prn22226sprB11Context _context;
        public List<string> Citites { get; set; } = new();
        [BindProperty]
        public string CitySelected { get; set; } = string.Empty;
        [BindProperty]
        public string CustomerName { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        public CreateModel(Prn22226sprB11Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Citites = _context.Customers.Select(e => e.City).Distinct().ToList();
        }

        public IActionResult OnPost()
        {
            try
            {
                var customer = new Q2.Entities.Customer
                {
                    CustomerName = CustomerName,
                    Email = Email,
                    City = CitySelected
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the customer: " + ex.Message);
                return Page();
            }

            return RedirectToPage("/Customer/List");
        }
    }
}
