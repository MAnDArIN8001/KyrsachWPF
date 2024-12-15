using System;
using System.Collections.Generic;

namespace RecepiesEverywhere.Models;

public partial class Favourite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RecipeId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
