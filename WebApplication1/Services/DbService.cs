using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TutorialWebApp.Exceptions;
using WebApplication1.Data;
using WebApplication1.DTOs;
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
        return 0;
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
