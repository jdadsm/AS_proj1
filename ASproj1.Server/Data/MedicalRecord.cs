using System.ComponentModel.DataAnnotations;

namespace ASproj1.Server.Data
{
    public class MedicalRecord
    {
        [Key]
        public string MedicalRecordNumber { get; set; }
        public string DiagnosisDetails { get; set; }
        public string TreatmentPlan { get; set; }
        public Patient Patient { get; set; }
        public MedicalRecord(string MedicalRecordNumber) 
        {
            this.MedicalRecordNumber = MedicalRecordNumber;
            this.DiagnosisDetails = string.Empty;
            this.TreatmentPlan = string.Empty;
        }
    }
}
