using System;
using System.Collections.Generic;

namespace BookstoreHandler.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? Namn { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}
