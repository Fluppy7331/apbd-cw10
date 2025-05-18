using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [MaxLength(100)]
    public string Description { get; set; } = null!;
    [MaxLength(100)]
    public string Type { get; set; } = null;
    
    public ICollection<PrescriptionMedicaments> PrescriptionMedicaments { get; set; } = null!;


}