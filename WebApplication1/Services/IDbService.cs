using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IDbService
{
    Task<int> AddPrescription(RequestPrescriptionInfoDto requestPrescription);
    Task<PatientPrescriptionsInfoDto> GetPrescriptions(int patientId);
}