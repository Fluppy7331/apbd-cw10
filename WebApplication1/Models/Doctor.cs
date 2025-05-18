using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    
    public ICollection<Prescription> Prescriptions { get; set; } = null!;
}