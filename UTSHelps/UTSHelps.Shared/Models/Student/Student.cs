using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{
    public class Student
    {
        public string studentID { get; set; }
        public DateTime? dob { get; set; }
        public string gender { get; set; }
        public string degree { get; set; }
        public string status { get; set; }
        public string first_language { get; set; }
        public string country_origin { get; set; }
        public string background { get; set; }
        public DateTime? created { get; set; }
        public int? creatorID { get; set; }
        public string degree_details { get; set; }
        public string alternative_contact { get; set; }
        public string preferred_name { get; set; }
    }
}
