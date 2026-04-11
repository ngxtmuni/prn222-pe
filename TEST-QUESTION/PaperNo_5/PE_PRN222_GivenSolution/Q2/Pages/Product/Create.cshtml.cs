using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper5Context _context;
        public List<Q2.Models.Category> Categories { get; set; } = new();
        [BindProperty]
        public string PNameSelected { get; set; } = string.Empty;
        [BindProperty]
        public decimal PriceSelected { get; set; }
        [BindProperty]
        public int QuantitySelected { get; set; }
        [BindProperty]
        public string StatusSelected { get; set; } = string.Empty;
        [BindProperty]
        public int CategoryIdSelected { get; set; }
        public CreateModel(PRN222_TestQuestion_Paper5Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }

        public IActionResult OnPost()
        {
            var product = new Q2.Models.Product
            {
                ProductName = PNameSelected,
                Price = PriceSelected,
                Quantity = QuantitySelected,
                Status = StatusSelected,
                CategoryId = CategoryIdSelected
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToPage("/Product/List");
        }
    }
}
