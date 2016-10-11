using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UTSHelps.Shared.Helpers;
using UTSHelps.Shared.Models;

namespace UTSHelps.Shared.Services
{
    public class BookingService : HelpsService
    {
        public BookingService() : base()
        {
        }

        public async Task<Response<Booking>> GetBookings(string studentId)
        {
            var queryString = "studentId=" + studentId;
            var response = await helpsClient.GetAsync("api/workshop/booking/search?" + queryString);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Response<Booking>>();
                return result;
            }
            return ResponseHelper.CreateErrorResponse<Booking>("An unknown error occured");
        }
    }
}
