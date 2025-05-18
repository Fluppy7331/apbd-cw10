using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<PrescriptionMedicaments> PrescriptionMedicaments { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    
    protected DatabaseContext()
    {
    }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(a =>
        {
            a.ToTable("Patient");
            a.HasKey(e => e.IdPatient);
            a.Property(e => e.FirstName).HasMaxLength(100);
            a.Property(e => e.LastName).HasMaxLength(100);
            a.Property(e => e.BirthDate).IsRequired();
        });

        modelBuilder.Entity<Doctor>(a =>
        {
            a.ToTable("Doctor");
            a.HasKey(e => e.IdDoctor);
            a.Property(e => e.FirstName).HasMaxLength(100);
            a.Property(e => e.LastName).HasMaxLength(100);
            a.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<Prescription>(a =>
        {
            a.ToTable("Prescription");
            a.HasKey(e => e.IdPrescription);
            a.Property(e => e.Date).IsRequired();
            a.Property(e => e.DueDate).IsRequired();
        });

        modelBuilder.Entity<PrescriptionMedicaments>(a =>
        {
            a.ToTable("PrescriptionMedicaments");
            a.HasKey(e => new {e.IdMedicament, e.IdPrescription});
            a.Property(e => e.Details).HasMaxLength(100);
        });
    }
}