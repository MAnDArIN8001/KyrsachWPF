using System;
using System.Collections.Generic;

namespace RecepiesEverywhere.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Text { get; set; }

    public string? Picture { get; set; }

    public int? AuthorId { get; set; }

    public int StatusId { get; set; }

    public virtual User? Author { get; set; }

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();

    public virtual Status Status { get; set; } = null!;
}
