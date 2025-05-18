using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class PrescriptionMedicaments
{
    [ForeignKey(nameof(Medicament))] 
    public int IdMedicament { get; set; }
    
    [ForeignKey(nameof(Prescription))] 
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; }
    
    
    public Medicament Medicament { get; set; } = null!;
    public Prescription Prescription { get; set; } = null!;
}