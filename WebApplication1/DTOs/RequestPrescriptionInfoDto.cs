

using WebApplication1.Models;

namespace WebApplication1.DTOs;

public class RequestPrescriptionInfoDto
{
    public PatientDto Patient { get; set; }
    public ICollection<MedicamentPrescriptionDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}
public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public DateTime BirthDate { get; set; }
}
public class MedicamentPrescriptionDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}
