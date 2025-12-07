using System;
using System.Collections.Generic;

namespace BookstoreHandler.Models;

public partial class Orderrad
{
    public int OrderradId { get; set; }

    public int? OrderId { get; set; }

    public string? Isbn13 { get; set; }

    public int? Antal { get; set; }

    public decimal? Pris { get; set; }

    public virtual Böcker? Isbn13Navigation { get; set; }

    public virtual Ordrar? Order { get; set; }
}
