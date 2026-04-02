using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project2.Entities;

namespace Project2.Pages.Instructor
{
    public class ListModel : PageModel
    {
        private readonly PePrn25sprB5Context _context;
        [BindProperty (SupportsGet = true)]
        public int? DepartmentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool? ContractType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }
        public List<Project2.Entities.Instructor> Instructors { get; set; } = new();
        public List<Project2.Entities.Department> Departments { get; set; } = new();
        public ListModel(PePrn25sprB5Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Project2.Entities.Instructor> query = _context.Instructors.Include(e => e.DepartmentNavigation);
            if (DepartmentId.HasValue && DepartmentId != 0)
            {
                query = query.Where(e => e.Department == DepartmentId);
            }

            if (ContractType.HasValue)
            {
                query = query.Where(e => e.IsFulltime == ContractType);
            }

            if (SortBy != null)
            {
                if (SortBy == "InstructorName")
                {
                    query = query.OrderBy(e => e.Fullname);
                }

                if (SortBy == "InstructorId")
                {
                    query = query.OrderBy(e => e.InstructorId);
                }

                if (SortBy == "ContractDate")
                {
                    query = query.OrderBy(e => e.ContractDate);
                }
            }

            Instructors = query.ToList();

            Departments = _context.Departments.OrderBy(e => e.DepartmentId).ToList();

            Console.WriteLine($"Value of ContractType: {ContractType}");
        }
    }
}
