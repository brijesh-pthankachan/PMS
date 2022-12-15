using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models.ViewModels;

public class RegisterViewModel
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Compare(nameof(Password))] public string ConfirmPassword { get; set; }
    
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