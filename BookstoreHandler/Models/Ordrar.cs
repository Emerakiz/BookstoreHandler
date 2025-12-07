using System;
using System.Collections.Generic;

namespace BookstoreHandler.Models;

public partial class Ordrar
{
    public int OrderId { get; set; }

    public int? KundId { get; set; }

    public int? ButikId { get; set; }

    public DateOnly? OrderDatum { get; set; }

    public decimal? TotalBelopp { get; set; }

    public virtual Butiker? Butik { get; set; }

    public virtual Kunder? Kund { get; set; }

    public virtual ICollection<Orderrad> Orderrads { get; set; } = new List<Orderrad>();
}
