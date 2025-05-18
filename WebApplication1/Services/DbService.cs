using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddPrescription(PrescriptionInfoDto prescription)
    {
        return 0;
    }
}