using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Patient
{
    public int IdPatient { get; set; }
    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    [MaxLength(100)] 
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = null!;
}