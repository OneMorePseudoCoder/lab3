using System;
using System.Collections.Generic;

namespace lab3.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
