using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Contract
{
    public class ListModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper2Context _context;
        public List<Q2.Models.Contract> Contracts { get; set; } = new();
        public List<string> Properties { get; set; } = new();
        [BindProperty (SupportsGet = true)]
        public string TitleSelected { get; set; } = string.Empty;
        [BindProperty (SupportsGet = true)]
        public string PropertySelected { get; set; } = string.Empty;
        public ListModel(PRN222_TestQuestion_Paper2Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Q2.Models.Contract> queryContracts = _context.Contracts.Include(c => c.Broker);
            if (!string.IsNullOrEmpty(TitleSelected))
            {
                queryContracts = queryContracts.Where(e => e.ContractTitle.Equals(TitleSelected));
            }
            if (!string.IsNullOrEmpty(PropertySelected))
            {
                queryContracts = queryContracts.Where(e => e.PropertyType.Equals(PropertySelected));
            }
            Contracts = queryContracts.OrderBy(e => e.ContractId).ToList();
        }
    }
}
