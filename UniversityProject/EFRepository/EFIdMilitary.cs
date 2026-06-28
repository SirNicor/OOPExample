using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRepository;

[Table("IdMilitary")]
public partial class EFIdMilitary
{
    public long Id { get; set; }

    public string LevelId { get; set; } = null!;

    public virtual ICollection<EFStudent> Students { get; set; } = new List<EFStudent>();
}
