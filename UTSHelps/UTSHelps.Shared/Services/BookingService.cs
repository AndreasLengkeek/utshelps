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

        public async Task<Response<Booking>> GetBookings(string studentId, bool? current = null)
        {
            var queryString = "studentId=" + studentId + "&active=true";

            if (current.HasValue)
            {
                if (current.Value)
                {
                    queryString += "&startingDtBegin=" + DateTime.Now.ToString(DateFormat) + "&startingDtEnd=" +
                                   DateTime.MaxValue.AddMonths(-1).ToString(DateFormat);
                }
                else
                {
                    queryString += "&startingDtBegin=" + DateTime.MinValue.AddYears(2000).ToString(DateFormat) +
                                   "&startingDtEnd=" + DateTime.Now.ToString(DateFormat);
                }
            }

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
