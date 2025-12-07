using System;
using System.Collections.Generic;

namespace BookstoreHandler.Models;

public partial class Kunder
{
    public int KundId { get; set; }

    public string? Förnamn { get; set; }

    public string? Efternamn { get; set; }

    public string? Email { get; set; }

    public string? TelefonNummer { get; set; }

    public virtual ICollection<Ordrar> Ordrars { get; set; } = new List<Ordrar>();
}
