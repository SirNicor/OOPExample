using Microsoft.EntityFrameworkCore;
using UCore;

namespace EFRepository;

public static class EntityForStudent
{
    public static void AddEntityForStudent(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");
            entity.HasIndex(e => e.PassportId, "IX_Student_PassportID").IsUnique();
            entity.Property(e => e.ChatId).HasMaxLength(255);
            entity.Property(e => e.Course).HasColumnName("CourseID");   
            entity.Property(e => e.MillitaryId).HasColumnName("MilitaryID");
            entity.Property(e => e.PassportId).HasColumnName("PassportID");
            entity.HasOne(d => d.Passport).WithOne(p => p.student)
                .HasForeignKey<Student>(d => d.PassportId)
                .HasConstraintName("FK_Student_PassportID_Passport_Id");

        });
    }
}