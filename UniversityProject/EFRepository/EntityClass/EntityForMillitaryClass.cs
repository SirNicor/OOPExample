using Microsoft.EntityFrameworkCore;
using UCore;

namespace EFRepository.EntityClass;

public static class EntityForMillitaryClass
{
    public static void AddEntityForMillitaryClass(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MillitaryClass>(entity =>
        {
            entity.ToTable("IdMillitary");
            entity.HasKey(e => e.MillitaryId);
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
        });
    }
}