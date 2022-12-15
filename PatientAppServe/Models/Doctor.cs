using PatientsAppServer.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 10)]
        public string DoctorPhoneNumber { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]

        public string Gender { get; set; }

        [Required]
        [StringLength(200)]
        public string Qualification { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public DateTime Dob { get; set; }
        public bool Availability { get; set; }
        public int? Rating { get; set; }

        public ApplicationUser ApplicationUsers { get; set; }
        public string Id { get; set; }
    }
}
