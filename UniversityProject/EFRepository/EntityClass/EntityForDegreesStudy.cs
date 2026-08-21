using Microsoft.EntityFrameworkCore;
using UCore;

namespace EFRepository.EntityClass;

public static class EntityForDegreesStudy
{
    public static void AddEntityForDegreesStudy(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DegreesStudyClass>(entity =>
        {
            entity.ToTable("DegreesStudy");
            entity.Property(e => e.LevelDegrees).HasMaxLength(255).HasConversion<string>();
        });
    }
}