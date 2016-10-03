using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{
    public class RegisterRequest
    {
        public string StudentId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Degree { get; set; }
        public int Status { get; set; }
        public string FirstLanguage { get; set; }
        public string CountryOrigin { get; set; }
        public string Background { get; set; }
        public string DegreeDetails { get; set; }
        public string AltContact { get; set; }
        public string PreferredName { get; set; }
        public bool HSC { get; set; }
        public string HSCMark { get; set; }
        public bool IELTS { get; set; }
        public string IELTSMark { get; set; }
        public bool TOEFL { get; set; }
        public string TOEFLMark { get; set; }
        public bool TAFE { get; set; }
        public string TAFEMark { get; set; }
        public bool CULT { get; set; }
        public string CULTMark { get; set; }
        public bool InsearchDEEP { get; set; }
        public string InsearchDEEPMark { get; set; }
        public bool Insearch { get; set; }
        public string InsearchMark { get; set; }
        public bool FoundationCourse { get; set; }
        public string FoundationCourseMark { get; set; }
    }
}
