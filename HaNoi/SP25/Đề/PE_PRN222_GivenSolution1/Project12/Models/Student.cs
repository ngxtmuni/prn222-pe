using System;
using System.Collections.Generic;
 
namespace Project12.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FullName { get; set; }

    public bool? Male { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Major { get; set; }

}
