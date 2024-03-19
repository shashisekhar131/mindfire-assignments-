using System;
using System.Collections.Generic;

namespace AspCoreCRUDLayered.DAL.DbModels;

public partial class Student
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }
}
