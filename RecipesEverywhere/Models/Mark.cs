using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecepiesEverywhere.Models;

public partial class Mark
{
    public int Id { get; set; }

    [Column("Mark")] public int MarkValue { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User Users { get; set; } = null!;

}
