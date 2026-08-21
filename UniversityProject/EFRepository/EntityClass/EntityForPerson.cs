using Microsoft.EntityFrameworkCore;
using UCore;

namespace EFRepository.EntityClass;

public static class EntityForPerson
{
    public static void AddEntityForPerson(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Student");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PassportId).HasColumnName("PassportID");
            entity.Property(e => e.MillitaryId).HasColumnName("MilitaryID");
            entity.HasOne(d => d.Millitary)
                .WithMany(p => p.Person)
                .HasForeignKey(d => d.MillitaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_MilitaryID_IdMilitary_Id");
        });
    }
}