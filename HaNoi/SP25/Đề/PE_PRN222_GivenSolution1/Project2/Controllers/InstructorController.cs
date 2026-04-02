using Microsoft.AspNetCore.Mvc;
using Project2.Models;

namespace Project2.Controllers
{
    public class InstructorController : Controller
    {
        private readonly PePrn25sprB5Context _context;

        public InstructorController(PePrn25sprB5Context context)
        {
            _context = context;
        }

        public IActionResult List(string departmentId, string contractType, string sortBy)
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;

            var instructors = _context.Instructors
                .Where(i => departmentId == null || i.DepartmentNavigation.DepartmentId.ToString() == departmentId)
                .Where(i => contractType == null ||
                       (contractType == "Fulltime" && i.IsFulltime == true) ||
                       (contractType == "Parttime" && i.IsFulltime == false));

            // Sorting
            switch (sortBy)
            {
                case "InstructorId":
                    instructors = instructors.OrderBy(i => i.InstructorId);
                    break;
                case "ContractDate":
                    instructors = instructors.OrderBy(i => i.ContractDate);
                    break;
                default:
                    instructors = instructors.OrderBy(i => i.Fullname);
                    break;
            }

            ViewBag.SelectedDepartment = departmentId;
            ViewBag.SelectedContract = contractType;
            ViewBag.SelectedSort = sortBy;

            return View(instructors.OrderBy(i => i.InstructorId).ToList());
        }

    }
}
