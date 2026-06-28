using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRepository;

[Table("DegreesStudy")]
public partial class EFDegreesStudy
{
    public long Id { get; set; }

    public string LevelDegrees { get; set; } = null!;
}
