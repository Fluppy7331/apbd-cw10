using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

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
        
        modelBuilder.Entity<Medicament>(a =>
        {
            a.ToTable("Medicament");
            a.HasKey(e => e.IdMedicament);
            a.Property(e => e.Name).HasMaxLength(100);
            a.Property(e => e.Description).HasMaxLength(100);
            a.Property(e => e.Type).HasMaxLength(100);
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = new DateTime(1980, 1, 1) }
        });
        
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor { IdDoctor = 1, FirstName = "AAA", LastName = "Nowak", Email = "aaa@nowak.pl" }
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription { IdPrescription = 1, Date = new DateTime(2012, 1, 1), DueDate = new DateTime(2012, 1, 1), IdPatient = 1, IdDoctor = 1 }
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament { IdMedicament = 1, Name = "AAA", Description = "AAA", Type = "Pill" }
        });
        
        modelBuilder.Entity<PrescriptionMedicaments>().HasData(new List<PrescriptionMedicaments>
        {
            new PrescriptionMedicaments { IdMedicament = 1, IdPrescription = 1, Dose = 10, Details = "Take one pill daily" }
        });
    }
}