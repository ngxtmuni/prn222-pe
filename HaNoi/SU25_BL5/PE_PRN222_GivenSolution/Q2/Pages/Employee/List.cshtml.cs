using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Q2.Entities;

namespace Q2.Pages.Employee
{
    public class ListModel : PageModel
    {
        private readonly PePrnSum25B5WaContext _context;
        public ListModel(PePrnSum25B5WaContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int DepartmentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int EmployeeId { get; set; }
        public List<Q2.Entities.Employee> Employees { get; set; }
        public List<EmployeeSkill> EmployeeSkill { get; set; }
        public List<Department> Departments { get; set; } = new();

        public void OnGet()
        {
            IQueryable<Q2.Entities.Employee> query = _context.Employees
                .Include(e => e.Department);
            if (DepartmentId != 0)
            {
                query = query.Where(e => e.DepartmentId == DepartmentId);
            }

            IQueryable<EmployeeSkill> query2 = _context.EmployeeSkills
                .Include(e => e.Skill);
            if (EmployeeId != 0)
            {
                query2 = query2.Where(e => e.EmployeeId == EmployeeId);
            }

            Employees = query.OrderBy(e => e.EmployeeId).ToList();
            EmployeeSkill = query2.OrderBy(e => e.SkillId).ToList();
            Departments = _context.Departments.ToList();
        }

        public void OnPost()
        {

        }
    }
}
