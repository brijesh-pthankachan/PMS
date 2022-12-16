using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientAppServe.Models;
using PatientsAppServer.Data;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PharmacyController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetParkedAppointments(int doctorId)
        {
            if (_db.Consultations == null) return Ok();
            var parkedPatients =
                _db.Consultations.Where(m => m.Status == "Parked" && m.Date.Date == DateTime.UtcNow.Date).ToList();
            return Ok(parkedPatients);

        }

        [HttpGet]
        public IActionResult GetCompletedAppointments(int doctorId)
        {
            if (_db.Consultations == null) return Ok();
            var completedPatients =
                _db.Consultations.Where(m => m.Status == "Completed" && m.Date.Date == DateTime.UtcNow.Date).ToList();
            return Ok(completedPatients);

        }

        [HttpPost("{appointmentId:int}")]
        public async Task<IActionResult> CompletePatient(Consultation model, int appointmentId)
        {
            if (_db.Consultations == null) return Ok();
            var patient = await _db.Consultations.FindAsync(appointmentId);
            if (patient != null)
            {
                
                patient.Status = "Completed";
            }

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
