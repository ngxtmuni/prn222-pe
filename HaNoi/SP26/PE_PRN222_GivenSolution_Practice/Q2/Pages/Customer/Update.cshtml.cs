using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Entities;

namespace Q2.Pages.Customer
{
    public class UpdateModel : PageModel
    {
        private readonly Prn22226sprB11Context _context;

        public Q2.Entities.Customer? Customer { get; set; }
        public List<string> Cities { get; set; } = new();

        [BindProperty]
        public int CustomerId { get; set; }

        [BindProperty]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string CitySelected { get; set; } = string.Empty;

        public UpdateModel(Prn22226sprB11Context context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Cities = _context.Customers.Select(e => e.City).Distinct().ToList();
            Customer = _context.Customers.FirstOrDefault(e => e.CustomerId == id);

            if (Customer == null)
            {
                return Page();
            }

            CustomerId = Customer.CustomerId;
            CustomerName = Customer.CustomerName;
            Email = Customer.Email;
            CitySelected = Customer.City;

            return Page();
        }

        public IActionResult OnPost()
        {
            Cities = _context.Customers.Select(e => e.City).Distinct().ToList();
            Customer = _context.Customers.FirstOrDefault(e => e.CustomerId == CustomerId);

            if (Customer == null)
            {
                ModelState.AddModelError(string.Empty, "Customer not found.");
                return Page();
            }

            // C1: lấy obj cũ và lưu luôn
            Customer.CustomerId = CustomerId;
            Customer.CustomerName = CustomerName;
            Customer.Email = Email;
            Customer.City = CitySelected;
            _context.SaveChanges();

            //var customer = new Q2.Entities.Customer      C2: khi update
            //{
            //    CustomerId = CustomerId,
            //    CustomerName = CustomerName,
            //    Email = Email,
            //    City = CitySelected
            //};
            //_context.Customers.Update(customer);
            //_context.SaveChanges();

            return Page();
        }
    }
}
