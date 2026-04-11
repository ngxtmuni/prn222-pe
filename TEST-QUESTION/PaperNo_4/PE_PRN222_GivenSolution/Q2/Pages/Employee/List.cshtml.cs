using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Employee
{
    public class ListModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper4Context _context;
        public List<Q2.Models.Employee> Employees { get; set; } = new();
        public List<Q2.Models.Department> Departments { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string SortSelected { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string DepartmentSelected { get; set; } = string.Empty;
        public ListModel(PRN222_TestQuestion_Paper4Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            IQueryable<Q2.Models.Employee> queryEmployee = _context.Employees.Include(e => e.Department);
            if (!string.IsNullOrEmpty(SortSelected))
            {
                switch (SortSelected)
                {
                    case "Name":
                        queryEmployee = queryEmployee.OrderBy(e => e.EmployeeName);
                        break;
                    case "HireDate":
                        queryEmployee = queryEmployee.OrderBy(e => e.HireDate);
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(DepartmentSelected))
            {
                queryEmployee = queryEmployee.Where(e => e.Department.DepartmentName == DepartmentSelected);
            }

            Employees = queryEmployee.ToList();
            Departments = _context.Departments.ToList();
        }
    }
}
