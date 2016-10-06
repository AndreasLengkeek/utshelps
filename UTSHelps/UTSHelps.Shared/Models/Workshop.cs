using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{
    public class Workshop
    {
        public int WorkshopId { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public string campus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int maximum { get; set; }
        public int WorkShopSetId { get; set; }
        public string WorkShopSetName { get; set; }
        public int? cutoff { get; set; }
        public string type { get; set; }
        public string DaysOfWeek { get; set; }
        public int BookingCount { get; set; }
        public int? NumOfWeeks { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? ProgramStartDate { get; set; }
        public DateTime? ProgramEndDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
