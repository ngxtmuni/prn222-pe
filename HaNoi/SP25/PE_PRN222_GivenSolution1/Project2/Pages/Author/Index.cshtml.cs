using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project2.Entities;

namespace Project2.Pages.Author
{
    public class IndexModel : PageModel
    {
        private readonly LibraryManagementContext _context;
        public Project2.Entities.Author? Author { get; set; } = new();
        public List<Project2.Entities.Book> Books{ get; set; } = new();
        public IndexModel(LibraryManagementContext context)
        {
            _context = context; 
        }
        public IActionResult OnGet(int? id)
        {
            Author = _context.Authors.Include(e => e.Books).FirstOrDefault(e => e.AuthorId == id);
            if (Author == null)
            {
                return NotFound();
            }

            Books = _context.Books.Where(e => e.Authors.Any(e => e.AuthorId == id)).ToList();

            return Page();
        }

        public IActionResult OnPostRemoveAuthor(int authorId, int bookId)
        {
            var author = _context.Authors.FirstOrDefault(e => e.AuthorId == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var book = _context.Books.Include(e => e.Authors).FirstOrDefault(e => e.BookId == bookId);
            if (book == null)
            {
                return NotFound();
            }

            book.Authors.Remove(author);
            _context.SaveChanges();

            return RedirectToPage(new { id = authorId });
        }
    }
}
