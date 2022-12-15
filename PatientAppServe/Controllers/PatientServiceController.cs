using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientAppServe.Models;
using PatientsAppServer.Data;
using PatientsAppServer.Models;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientServiceController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("{patientId:int}")]
        public IActionResult GetPendingAppointments(int patientId)
        {
            if (_db.Consultations == null) return Ok();
            var incompleteAppointments =
                _db.Consultations.Where(m => m.PatientId == patientId && m.Status == "Incomplete").ToList();
                
            return Ok(incompleteAppointments);

        }
        
        [HttpGet("{id}")]
        public IActionResult GetLinkedAccounts(string id)
        {
            var linkedAccounts = _db.Patients.Where(m => m.Id == id).ToList();
            return Ok(linkedAccounts);

        }
        
        [HttpGet("{patientId:int}")]
        public IActionResult GetConsultationDetails(int patientId)
        {
            if (_db.Consultations == null) return Ok();
            var completedConsultations =
                _db.Consultations.Where(m => m.PatientId == patientId && m.Status == "Completed").ToList();
                
            return Ok(completedConsultations);

        }

        [HttpPost("{patientId:int}")]

        public async Task<IActionResult> BookAppointment(Consultation model, int patientId)
                {
                    if (_db.Consultations == null) return Ok();
                    await _db.Consultations.AddAsync(new Consultation()
                    {
                        Date = model.Date,
                        Status = "Incomplete",
                        PatientId = patientId,
                        DoctorId = model.DoctorId,
                        ConsultationFee = model.ConsultationFee,
                        ConsultationMode = model.ConsultationMode,
                        Session = model.Session
                        
                    });
                    await _db.SaveChangesAsync();
                    return Ok();
                }

    }
}
