using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRepository;

[Table("Passport")]
public partial class EFPassport
{
    public long Id { get; set; }

    public string Serial { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public DateOnly BirthData { get; set; }

    public long AddressId { get; set; }

    public string PlaceReceipt { get; set; } = null!;

    public virtual EFAddress EfAddress { get; set; } = null!;

    public virtual EFStudent? Student { get; set; }
}
