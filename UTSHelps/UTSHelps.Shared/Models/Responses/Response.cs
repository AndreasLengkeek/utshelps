using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSHelps.Shared.Models
{

    public class Response<T> : GenericResponse
    {
        public List<T> Results { get; set; }
    }
}
