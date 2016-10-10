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
        public int Gender { get; set; }
        public int Status { get; set; }
        public string FirstLanguage { get; set; }
        public string CountryOrigin { get; set; }
        public string Background { get; set; }
        public string DegreeDetails { get; set; }
        public string AltContact { get; set; }
        public string PreferredName { get; set; }
    }
}
