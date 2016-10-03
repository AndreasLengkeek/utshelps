using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int workshopId { get; set; }
        public string studentId { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public int campusID { get; set; }
        public DateTime starting { get; set; }
        public DateTime ending { get; set; }
        public int maximum { get; set; }
        public int? cutoff { get; set; }
        public int? attended { get; set; }
        public string type { get; set; }
        public int WorkShopSetID { get; set; }
        public string WorkShopSetName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string notes { get; set; }
    }
}
