using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Contract
{
    public class CreateModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper2Context _context;
        [BindProperty]
        public string ContractTitleSelected { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyTypeSelected { get; set; } = string.Empty;
        [BindProperty]
        public DateOnly SigningDateSelected { get; set; }
        [BindProperty]
        public DateOnly ExpirationDateSelected { get; set; }
        [BindProperty]
        public int BrokerIdSelected { get; set; }
        [BindProperty]
        public decimal ValueSelected { get; set; } = 0;
        public List<Q2.Models.Broker> Brokers { get; set; } = new();
        public List<string> Properties { get; set; } = new();
        public CreateModel(PRN222_TestQuestion_Paper2Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Properties = _context.Contracts.Select(e => e.PropertyType).Distinct().ToList();
            Brokers = _context.Brokers.ToList();
        }

        public IActionResult OnPost()
        {
            Properties = _context.Contracts.Select(e => e.PropertyType).Distinct().ToList();
            Brokers = _context.Brokers.ToList();
            if (string.IsNullOrWhiteSpace(ContractTitleSelected))
            {
                ModelState.AddModelError(nameof(ContractTitleSelected), "Contract title is required.");
            }
            if (ExpirationDateSelected <= SigningDateSelected)
            {
                ModelState.AddModelError(nameof(ExpirationDateSelected), "Expiration date must be greater than signing date.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contract = new Q2.Models.Contract
            {
                ContractTitle = ContractTitleSelected,
                PropertyType = PropertyTypeSelected,
                SigningDate = SigningDateSelected,
                ExpirationDate = ExpirationDateSelected,
                BrokerId = BrokerIdSelected,
                Value = ValueSelected
            };
            _context.Contracts.Add(contract);
            _context.SaveChanges();
            return RedirectToPage("/Contract/List");
        }
    }
}
