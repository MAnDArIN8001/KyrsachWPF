using System;
using System.Collections.Generic;

namespace RecepiesEverywhere.Models;

public partial class Mark
{
    public int Id { get; set; }

    public int Mark1 { get; set; }

    public int RecipeId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
