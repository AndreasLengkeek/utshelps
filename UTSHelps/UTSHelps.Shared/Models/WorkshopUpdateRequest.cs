using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{
    public class WorkshopUpdateRequest
    {
        public int workshopId { get; set; }
        public string studentId { get; set; }
        public string userId { get; set; }
        public string notes { get; set; }
    }
}
