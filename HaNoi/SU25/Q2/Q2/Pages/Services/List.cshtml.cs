using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Entities;

namespace Q2.Pages.Services
{
    public class ListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? RoomTitle { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? FeeType { get; set; }

        public List<Service> Services = new();

        public Prnsum25B123Context _context;
        public ListModel(Prnsum25B123Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            IQueryable<Service> query = _context.Services
                .Include(s => s.EmployeeNavigation);

            if (!string.IsNullOrWhiteSpace(RoomTitle))
            {
                var roomTitle = RoomTitle.ToLower();
                query = query.Where(m => (m.RoomTitle ?? "").ToLower().Contains(roomTitle));
            }

            if (!string.IsNullOrWhiteSpace(FeeType))
            {
                var feeType = FeeType.ToLower();
                query = query.Where(s => (s.FeeType ?? "").ToLower().Contains(feeType));
            }

            Services = query.OrderBy(s => s.Id).ToList();
        }
    }
}
