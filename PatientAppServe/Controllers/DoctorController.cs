using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Data;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(Doctor model)
        {
            var existingDoctor = await _context.Doctors.FindAsync(model.Id);

            if (existingDoctor != null)
            {
                existingDoctor.Gender = model.Gender;
                existingDoctor.DoctorPhoneNumber = model.DoctorPhoneNumber;
                existingDoctor.Availability = model.Availability;
                existingDoctor.Qualification = model.Qualification;
                existingDoctor.Experience = model.Experience;
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }
        
     
  

       
    }
}
