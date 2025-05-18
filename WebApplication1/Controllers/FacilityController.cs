using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorialWebApp.Exceptions;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IDbService _dbService;
        public FacilityController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpPost("addPrescription")]
        public async Task<IActionResult> AddPrescription([FromBody] RequestPrescriptionInfoDto requestPrescription)
        {
            if (requestPrescription == null)
            {
                return BadRequest("Prescription data is required.");
            }
            
            int newId;
            try
            {
                newId = await _dbService.AddPrescription(requestPrescription);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ConflictException e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction(nameof(AddPrescription), new { id = newId }, requestPrescription);
        }
        
        [HttpGet("getPrescriptions/{patientId}")]
        public async Task<IActionResult> GetPrescriptions(int patientId)
        {
            if (patientId <= 0)
            {
                return BadRequest("Invalid patient ID.");
            }
            
            PatientPrescriptionsInfoDto prescriptions;
            try
            {
                prescriptions = await _dbService.GetPrescriptions(patientId);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            
            return Ok(prescriptions);
        }
     
        
        
        
        
        
    }
}
