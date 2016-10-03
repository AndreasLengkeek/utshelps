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
    public class MiscService : HelpsService
    {
        public MiscService() : base()
        {
        }

        public async Task<Response<Campus>> GetCampus(int campusId)
        {
            TestConnection();

            var response = await helpsClient.GetAsync("api/misc/campus/true");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Response<Campus>>();

                if (result.IsSuccess)
                {
                    var campus = result.Results.Where(c => c.id == campusId).FirstOrDefault();
                    return ResponseHelper.CreateResponseDetail(campus);
                }
                return ResponseHelper.CreateErrorResponse<Campus>("Could not find campus records");
            }
            return ResponseHelper.CreateErrorResponse<Campus>("An unkown error occured");
        }
    }
}
