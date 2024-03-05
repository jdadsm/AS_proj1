using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASproj1.Server.Data
{
    public class MedicalRecord
    {
        int MedicalRecordNumber { get; set; };
        string DiagnosisDetails { get; set; };
        string TreatmentPlan { get; set; };
    }
}
