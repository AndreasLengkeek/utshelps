using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models.Generic
{

    public class Response<T>
    {
        public List<T> Results { get; set; }
        public bool IsSuccess { get; set; }
        public string DisplayMessage { get; set; }
    }
}
