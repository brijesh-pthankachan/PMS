using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientAppServe.Models.ViewModels;
using PatientsAppServer.Data;
using PatientsAppServer.Models;


namespace PatientAppServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Patients.ToListAsync());
        }
        
        
        [HttpGet("{patientId}")]

        public async Task<IActionResult> Get(int patientId)
        {
            return Ok(await _db.Patients.FindAsync(patientId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterViewModel model)
        {
            var newPatient = new Patient
            {
                Aadhar = model.Aadhar,
                BloodGroup = model.BloodGroup,
                Dob = model.Dob,
                Gender = model.Gender,
                Pincode = model.Pincode,
                FirstName = model.FirstName,
                Place = model.Place,
                HouseNo = model.HouseNo,
                LastName = model.LastName,
                Relation = model.Relation,
                EmergencyContactNumber = model.EmergencyContactNumber,
                PatientPhoneNumber = _userManager.FindByEmailAsync(model.Email).Result.Id,
                Id = _userManager.FindByEmailAsync(model.Email).Result.Id
                
            };
            await _db.Patients.AddAsync(newPatient);
            await _db.SaveChangesAsync();
            return Ok();
        }
        
    


        [HttpPut]
        public async Task<IActionResult> Update(Patient model)
        {
            var existingPatient = await _db.Patients.FindAsync(model.Id);

            if (existingPatient != null)
            {
                existingPatient.Gender = model.Gender;
                existingPatient.EmergencyContactNumber = model.EmergencyContactNumber;
                existingPatient.Pincode = model.Pincode;
                existingPatient.Place = model.Place;
                existingPatient.HouseNo = model.HouseNo;
            }
            
            await _db.SaveChangesAsync();
            return Ok();
        }
        
        

    }
}
