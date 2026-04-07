using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Entities;

namespace Q2.Pages.Customer
{
    public class ListModel : PageModel
    {
        private readonly Prn22226sprB11Context _context;
        public List<Q2.Entities.Customer> Customers { get; set; } = new();
        public List<Q2.Entities.Order> Orders { get; set; } = new();
        public List<string> Cities { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string CitySelected { get; set; } = string.Empty;

        public ListModel(Prn22226sprB11Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Q2.Entities.Customer> queryCustomer = _context.Customers;
            IQueryable<Q2.Entities.Order> queryOrder = _context.Orders;

            Cities = _context.Customers.Select(e => e.City).Distinct().ToList();

            if (!string.IsNullOrEmpty(CitySelected))
            {
                queryCustomer = queryCustomer.Where(e => e.City == CitySelected);
            }

            Customers = queryCustomer.ToList();
            Orders = queryOrder.ToList();
        }
    }
}
