using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Employee
{
    public class UpdateModel : PageModel
    {
        private readonly PRN222_TestQuestion_Paper4Context _context;
        public List<Q2.Models.Department> Departments { get; set; } = new();
        [BindProperty]
        public int EmployeeId { get; set; }
        [BindProperty]
        public string EmployeeNameSelected { get; set; } = string.Empty;
        [BindProperty]
        public string PositionSelected { get; set; } = string.Empty;
        [BindProperty]
        public int? DepartmentId { get; set; }
        [BindProperty]
        public DateOnly HireDateSelected { get; set; }
        public Q2.Models.Employee? EmployeeQuery { get; set; } = new();
        public UpdateModel(PRN222_TestQuestion_Paper4Context context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            Departments = _context.Departments.ToList();
            EmployeeQuery = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (EmployeeQuery != null)
            {
                EmployeeId = EmployeeQuery.EmployeeId;
                EmployeeNameSelected = EmployeeQuery.EmployeeName;
                PositionSelected = EmployeeQuery.Position;
                HireDateSelected = EmployeeQuery.HireDate;
                DepartmentId = EmployeeQuery.DepartmentId;
            }
        }

        public IActionResult OnPost()
        {
            Departments = _context.Departments.ToList();

            var department = _context.Departments.FirstOrDefault(e => e.DepartmentId == DepartmentId);
            if (department == null)
            {
                ModelState.AddModelError("DepartmentId", "Department not found.");
                return Page();
            }

            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = EmployeeNameSelected;
            employee.Position = PositionSelected;
            employee.HireDate = HireDateSelected;
            employee.DepartmentId = department.DepartmentId;

            _context.SaveChanges();
            return RedirectToPage("/Employee/List");
        }
    }
}
