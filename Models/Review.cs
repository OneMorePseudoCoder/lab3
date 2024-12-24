using System;
using System.Collections.Generic;

namespace lab3.Models;

public partial class Review
{
    public int RewiewId { get; set; }

    public decimal Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
