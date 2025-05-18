

using WebApplication1.Models;

namespace WebApplication1.DTOs;

public class PrescriptionInfoDto
{
    public PatientDto Patient { get; set; }
    public ICollection<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}
public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public DateTime BirthDate { get; set; }
}
public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Type { get; set; } 
}
