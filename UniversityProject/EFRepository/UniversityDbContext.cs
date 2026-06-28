using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFRepository;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EFAddress> Addresses { get; set; }

    public virtual DbSet<EFDegreesStudy> DegreesStudies { get; set; }

    public virtual DbSet<EFIdMilitary> IdMilitaries { get; set; }

    public virtual DbSet<EFPassport> Passports { get; set; }

    public virtual DbSet<EFStudent> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityDB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EFAddress>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.AddressString).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.HouseNumber).HasMaxLength(255);
            entity.Property(e => e.Street).HasMaxLength(255);
        });

        modelBuilder.Entity<EFDegreesStudy>(entity =>
        {
            entity.ToTable("DegreesStudy");

            entity.Property(e => e.LevelDegrees).HasMaxLength(255);
        });

        modelBuilder.Entity<EFIdMilitary>(entity =>
        {
            entity.ToTable("IdMilitary");

            entity.Property(e => e.LevelId)
                .HasMaxLength(255)
                .HasColumnName("LevelID");
        });

        modelBuilder.Entity<EFPassport>(entity =>
        {
            entity.ToTable("Passport");

            entity.HasIndex(e => new { e.Serial, e.Number }, "IndexSerialNumber").IsUnique();

            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.Number).HasMaxLength(6);
            entity.Property(e => e.PlaceReceipt).HasMaxLength(255);
            entity.Property(e => e.Serial).HasMaxLength(4);

            entity.HasOne(d => d.EfAddress).WithMany(p => p.Passports)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passport_AddressId_Address_Id");
        });

        modelBuilder.Entity<EFStudent>(entity =>
        {
            entity.ToTable("Student");

            entity.HasIndex(e => e.PassportId, "IX_Student_PassportID").IsUnique();

            entity.Property(e => e.ChatId).HasMaxLength(255);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.MilitaryId).HasColumnName("MilitaryID");
            entity.Property(e => e.PassportId).HasColumnName("PassportID");

            entity.HasOne(d => d.Military).WithMany(p => p.Students)
                .HasForeignKey(d => d.MilitaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_MilitaryID_IdMilitary_Id");

            entity.HasOne(d => d.EfPassport).WithOne(p => p.Student)
                .HasForeignKey<EFStudent>(d => d.PassportId)
                .HasConstraintName("FK_Student_PassportID_Passport_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
