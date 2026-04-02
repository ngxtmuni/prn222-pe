using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project2.Entities;

namespace Project2.Pages
{
    public class AuthorModel : PageModel
    {
        private readonly LibraryManagementContext _context;
        [BindProperty (SupportsGet = true)]
        public int? BookId { get; set; }
        public List<Author> Authors { get; set; }
        public AuthorModel(LibraryManagementContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Author> query = _context.
                .Include(a => a.);
            if (BookId != 0)
            {
                query = query.Where(a => a.BookId == BookId);
            }

            Authors = query.OrderBy()
        }
    }
}
