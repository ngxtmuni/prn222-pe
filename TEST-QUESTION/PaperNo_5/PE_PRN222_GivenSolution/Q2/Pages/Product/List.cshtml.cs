using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Product
{
    public class ListModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper5Context _context;
        public List<Q2.Models.Product> Products { get; set; } = new();
        public List<Q2.Models.Category> Categories { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string SortSelected { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public int CategoryIdSelected { get; set; }

        public ListModel(PRN222_TestQuestion_Paper5Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            IQueryable<Q2.Models.Product> queryProducts = _context.Products.Include(e => e.Category);
            if (CategoryIdSelected != 0)
            {
                queryProducts = queryProducts.Where(e => e.CategoryId == CategoryIdSelected);
            }

            if (!string.IsNullOrEmpty(SortSelected))
            {
                switch (SortSelected)
                {
                    case "Name":
                        queryProducts = queryProducts.OrderBy(e => e.ProductName);
                        break;
                    case "Price":
                        queryProducts = queryProducts.OrderBy(e => e.Price);
                        break;
                    default:
                        break;
                }
            }

            Products = queryProducts.ToList();
        }
    }
}
