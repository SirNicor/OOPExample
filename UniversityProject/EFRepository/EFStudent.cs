using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRepository;

[Table("Student")]
public partial class EFStudent
{
    public long Id { get; set; }

    public long PassportId { get; set; }

    public long MilitaryId { get; set; }

    public bool CriminalRecord { get; set; }

    public long CourseId { get; set; }

    public long? SkipHours { get; set; }

    public long? CountOfExamsPassed { get; set; }

    public long? CreditScores { get; set; }

    public string? ChatId { get; set; }

    public virtual EFIdMilitary Military { get; set; } = null!;

    public virtual EFPassport EfPassport { get; set; } = null!;
}
