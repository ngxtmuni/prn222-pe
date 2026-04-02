using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project2.Entities;

namespace Project2.Pages
{
    public class AuthorModel : PageModel
    {
        private readonly LibraryManagementContext _context;
        [BindProperty(SupportsGet = true)]
        // public int BookId { get; set; } = 0; //Cách 1: cho bookID = 0 sẵn để lấy tất cả author
        public int? BookId { get; set; } // Cách 2: để bookID là null nếu không có query string, và chỉ lọc khi bookID có giá trị
        public List<Project2.Entities.Author> Authors { get; set; } = new();
        public List<Book> Books { get; set; } = new();
        public AuthorModel(LibraryManagementContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Project2.Entities.Author> query = _context.Authors
                .Include(a => a.Books);
            //if (BookId != 0) 
            if (BookId.HasValue && BookId.Value != 0) // Cách 2
            {
                query = query.Where(a => a.Books.Any(b => b.BookId == BookId));
            }

            Books = _context.Books.ToList();

            Authors = query.OrderBy(e => e.AuthorId).ToList();
        }
    }
}
