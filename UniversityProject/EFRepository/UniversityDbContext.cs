using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UCore;

namespace EFRepository;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext() { }
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<MillitaryClass> IdMilitaries { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");
            entity.HasKey(e => e.AddressId);
            entity.Property(e => e.AddressId).HasColumnName("Id");
            entity.Property(e => e.AddressString).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.HouseNumber).HasMaxLength(255);
            entity.Property(e => e.Street).HasMaxLength(255);
        });
        modelBuilder.Entity<DegreesStudyClass>(entity =>
        {
            entity.ToTable("DegreesStudy");

            entity.Property(e => e.LevelDegrees).HasMaxLength(255).HasConversion<string>();
        });
        modelBuilder.Entity<MillitaryClass>(entity =>
        {
            entity.ToTable("IdMillitary");
            entity.HasKey(e => e.MillitaryId);
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
        });
        modelBuilder.Entity<Passport>(entity =>
        {
            entity.ToTable("Passport");
            entity.HasKey(e => e.PassportId);   
            entity.Property(e => e.PassportId).HasColumnName("Id");
            entity.HasIndex(e => new { e.Serial, e.Number }, "IndexSerialNumber").IsUnique();
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.Number).HasMaxLength(6);
            entity.Property(e => e.PlaceReceipt).HasMaxLength(255);
            entity.Property(e => e.Serial).HasMaxLength(4);
            entity.HasOne(d => d.Address).WithMany(p => p.Passports)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passport_AddressId_Address_Id");
        });
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    private string _getConnectionString;
}
