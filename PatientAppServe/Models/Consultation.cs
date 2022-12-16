using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models
{
    public class Consultation
    {
        [Key]
        public int AppointmentId { get; set; }
        public Patient? Patients { get; set; }
        public int PatientId { get; set; }
        public Doctor? Doctors { get; set; }
        public int DoctorId { get; set; }



        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Session { get; set; }

        public string Status { get; set; }
        [Required]
        public float ConsultationFee { get; set; }

        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public string? Radiology { get; set; }
        public string? LabTest { get; set; }
        public string? Remarks { get; set; }
        [Required]
        public string ConsultationMode { get; set; }
    }
}
