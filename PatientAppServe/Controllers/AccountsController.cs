using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientAppServe.Models;
using PatientAppServe.Models.ViewModels;
using PatientsAppServer.Data;
using PatientsAppServer.Models;

namespace PatientAppServe.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
     private readonly ApplicationDbContext _db;
     private readonly RoleManager<IdentityRole> _roleManager;
     private readonly SignInManager<ApplicationUser> _signInManager;
     private readonly UserManager<ApplicationUser> _userManager;

     public AccountsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
     {
         _db = db;
         _userManager = userManager;
         _signInManager = signInManager;
         _roleManager = roleManager;
     }

     [HttpPost]
     public async Task<IActionResult> NewPatient(RegisterViewModel model)
     {
         var newPatient = new ApplicationUser
         {
             Email = model.Email,
             PhoneNumber = model.PatientPhoneNumber,
             UserName = Guid.NewGuid().ToString().Replace("-", "").ToLower()
         };

         var result = await _userManager.CreateAsync(newPatient, model.Password);
         if (!result.Succeeded) return NotFound();
         await _userManager.AddToRoleAsync(newPatient, "User");

         await _db.Patients.AddAsync(new Patient
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
             Relation = "Self",
             EmergencyContactNumber = model.EmergencyContactNumber,
             PatientPhoneNumber = model.PatientPhoneNumber,
             Id = _userManager.FindByEmailAsync(model.Email).Result.Id
                 
         });
         await _db.SaveChangesAsync();
         
         return Ok();

     }
  
     [HttpPost]
     public async Task<IActionResult> NewDoctor(DoctorRegisterModel model)
     {
         var newDoctor = new ApplicationUser()
         {
            Email = model.Email,
            PhoneNumber = model.DoctorPhoneNumber,
            UserName = Guid.NewGuid().ToString().Replace("-", "").ToLower()
         };

         var result = await _userManager.CreateAsync(newDoctor, model.Password);
         if (!result.Succeeded) return NotFound();
         await _userManager.AddToRoleAsync(newDoctor, "Doctor");

         await _db.Doctors.AddAsync(new Doctor
         {
             Id = _userManager.FindByEmailAsync(model.Email).Result.Id,
             Availability    = model.Availability,
             Dob = model.Dob,
             Experience = model.Experience,
             Gender = model.Gender,
             DoctorPhoneNumber = model.DoctorPhoneNumber,
             Qualification = model.Qualification,
             Rating = null,
             FirstName = model.FirstName,
             LastName = model.LastName
             
         });

         await _db.SaveChangesAsync();
         return Ok();

     }
   
     [HttpPost]
     public async Task<IActionResult> GenerateData()
     {
         await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
         await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
         await _roleManager.CreateAsync(new IdentityRole { Name = "Doctor" });

         var users = await _userManager.GetUsersInRoleAsync("Admin");
         if (users.Count != 0) return Ok("Data Generated");
         var adminUser = new ApplicationUser
         {
             Email = "admin@123.com",
             UserName = "admin"
         };
         var res = await _userManager.CreateAsync(adminUser, "Pass@123");
         await _userManager.AddToRoleAsync(adminUser, "admin");

         return Ok("Data Generated");
     }
   
}