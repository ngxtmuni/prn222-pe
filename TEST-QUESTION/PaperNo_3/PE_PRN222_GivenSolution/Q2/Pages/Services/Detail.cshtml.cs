using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Services
{
    public class DetailModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper3Context _context;
        public Q2.Models.Service? ServiceModel { get; set; } = new();
        public DetailModel(PRN222_TestQuestion_Paper3Context context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            ServiceModel = _context.Services.FirstOrDefault(s => s.ServiceId == id);
        }
    }
}
