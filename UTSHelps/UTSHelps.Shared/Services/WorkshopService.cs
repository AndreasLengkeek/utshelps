using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTSHelps.Shared.Models;
using System.Net.Http;
using UTSHelps.Shared.Helpers;

namespace UTSHelps.Shared.Services
{
    public class WorkshopService : HelpsService
    {
        public async Task<Response<WorkshopSet>> GetWorkshopSets()
        {
            // test connection

            var response = await helpsClient.GetAsync("api/workshop/workshopSets/true");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsAsync<Response<WorkshopSet>>();
                return results;
            }

            return ResponseHelper.CreateErrorResponse<WorkshopSet>("Could not find records");
        }
    }
}
