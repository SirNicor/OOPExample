using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRepository;

[Table("Address")]
public partial class EFAddress
{
    public long Id { get; set; }

    public string? AddressString { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? HouseNumber { get; set; }

    public virtual ICollection<EFPassport> Passports { get; set; } = new List<EFPassport>();
}
