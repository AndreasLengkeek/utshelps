using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTSHelps.Shared.Models;

namespace UTSHelps.Shared.Helpers
{
    public class ResponseHelper
    {
        public static Response<T> CreateErrorResponse<T>(string message)
        {
            return new Response<T> {
                IsSuccess = false,
                DisplayMessage = message
            };
        }
    }
}
