using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TutorialWebApp.Exceptions;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> AddPrescription(RequestPrescriptionInfoDto requestPrescription)
    {
        if (requestPrescription.Medicaments.Count > 10)
            throw new ConflictException("Recepta może obejmować maksymalnie 10 leków");

        if (requestPrescription.DueDate < requestPrescription.Date)
            throw new ConflictException("DueDate musi być większe lub równe Date");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var patient =
                await _context.Patients.FirstOrDefaultAsync(e => e.IdPatient == requestPrescription.Patient.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = requestPrescription.Patient.FirstName,
                    LastName = requestPrescription.Patient.LastName,
                    BirthDate = requestPrescription.Patient.BirthDate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
            
            var doctor = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == requestPrescription.IdDoctor);
            Console.WriteLine(doctor is null);
            if (doctor == null)
                throw new NotFoundException("Podany lekarz nie istnieje");
            var medicaments = await _context.Medicaments
                .Where(e => requestPrescription.Medicaments.Select(m => m.IdMedicament).Contains(e.IdMedicament))
                .ToListAsync();
            if (medicaments.Count != requestPrescription.Medicaments.Count)
                throw new NotFoundException("Niektóre z leków nie istnieją w bazie");
            var prescription = new Prescription
            {
                Date = requestPrescription.Date,
                DueDate = requestPrescription.DueDate,
                IdDoctor = doctor.IdDoctor,
                IdPatient = patient.IdPatient
            };
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            foreach (var medicamentDto in requestPrescription.Medicaments)
            {
                var prescriptionMedicament = new PrescriptionMedicaments
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Details
                };
                _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return prescription.IdPrescription;
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<PatientPrescriptionsInfoDto> GetPrescriptions(int patientId)
    {
        var patientPrescriptions = await _context.Patients
            .Where(e => e.IdPatient == patientId)
            .Select(e => new PatientPrescriptionsInfoDto
            {
                IdPatient = e.IdPatient,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Prescriptions = e.Prescriptions.Select(a => new PrescriptionDto
                {
                    IdPrescription = a.IdPrescription,
                    Date = a.Date,
                    DueDate = a.DueDate,
                    Medicaments = a.PrescriptionMedicaments.Join(
                        _context.Medicaments,
                        pm => pm.IdMedicament,
                        m => m.IdMedicament,
                        (pm, m) => new MedicamentInfoDto
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = m.Name,
                            Dose = pm.Dose,
                            Description = m.Description,
                        }).ToList(),
                    Doctor = new DoctorDto
                    {
                        IdDoctor = a.Doctor.IdDoctor,
                        FirstName = a.Doctor.FirstName,
                    }
                }).OrderBy(a => a.DueDate).ToList()
            }).FirstOrDefaultAsync();

        if (patientPrescriptions == null)
        {
            throw new NotFoundException("Patient not found");
        }

        return patientPrescriptions;
    }
}