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

        public static GenericResponse CreateGenericErrorResponse(string message)
        {
            return new GenericResponse {
                IsSuccess = false,
                DisplayMessage = message
            };
        }

        public static GenericResponse Success()
        {
            return new GenericResponse {
                IsSuccess = true
            };
        }

        public static Response<T> CreateResponseDetail<T>(T result)
        {
            if (result == null)
                return CreateErrorResponse<T>("Could not find " + nameof(T) + " with that id");

            return new Response<T> {
                Results = new List<T> { result },
                IsSuccess = true
            };
        }

		public static WaitListed CreateWaitListErrorResponse(string message)
		{
			return new WaitListed
			{
				IsSuccess = false,
				DisplayMessage = message
			};
		}

		public static WaitListCount GetWaitListedErrorResponse(string message)
		{
			return new WaitListCount
			{
				IsSuccess = false,
				DisplayMessage = message
			};
		}
    }
}
