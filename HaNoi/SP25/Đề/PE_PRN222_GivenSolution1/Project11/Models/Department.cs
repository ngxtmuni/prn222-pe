using System;
using System.Collections.Generic;

namespace Project11.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
