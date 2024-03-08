using System.ComponentModel.DataAnnotations;

namespace ASproj1.Server.Data
{
    public class MedicalRecord
    {
        [Key]
        public string MedicalRecordNumber { get; set; }
        public string DiagnosisDetails { get; set; }
        public string TreatmentPlan { get; set; }
        public MedicalRecord() 
        {
            this.MedicalRecordNumber = Guid.NewGuid().ToString();
            this.DiagnosisDetails = string.Empty;
            this.TreatmentPlan = string.Empty;
        }
    }
}
