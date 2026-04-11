using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Services
{
    public class ListModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper3Context _context;
        public List<Q2.Models.Service> Services { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string RoomTitleSelected { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string FeeTypeSelected { get; set; } = string.Empty;
        public ListModel(PRN222_TestQuestion_Paper3Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Q2.Models.Service> queryService = _context.Services;
            if (!string.IsNullOrEmpty(RoomTitleSelected))
            {
                queryService = queryService.Where(s => s.RoomTitle == RoomTitleSelected);
            }

            if (!string.IsNullOrEmpty(FeeTypeSelected))
            {
                queryService = queryService.Where(s => s.FeeType == FeeTypeSelected);
            }

            Services = queryService.OrderBy(s => s.ServiceId).ToList();
        }
    }
}
