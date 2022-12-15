using Microsoft.EntityFrameworkCore;
using PatientsAppServer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAppServe.Models
{
    [Index(nameof(Aadhar), IsUnique = true)]
    public class Patient
    {

        [Key]
        [Required]
        public int PatientId { get; set; }
        [Required]
        public ApplicationUser UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public String Id { get; set; }
        public string PatientPhoneNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(25)]
        public string HouseNo { get; set; }
        [Required]
        [MaxLength(25)]
        public string Place { get; set; }
        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public int Pincode { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string EmergencyContactNumber { get; set; }
        public string BloodGroup { get; set; }
        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        public long Aadhar { get; set; }
        [Required]
        public string Relation { get; set; }

    }

}

