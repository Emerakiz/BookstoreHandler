using System;
using System.Collections.Generic;

namespace BookstoreHandler.Models;

public partial class Förlag
{
    public int FörlagId { get; set; }

    public string? Namn { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}
