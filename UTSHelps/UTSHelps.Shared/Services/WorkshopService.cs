﻿using System;
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
            TestConnection();

            var response = await helpsClient.GetAsync("api/workshop/workshopSets/true");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsAsync<Response<WorkshopSet>>();
                return results;
            }
            
            return ResponseHelper.CreateErrorResponse<WorkshopSet>("Could not find workshop sets");
        }

        public async Task<Response<Workshop>> GetWorkshops(string workshopSetId)
        {
            TestConnection();

            var queryString = "workshopSetId=" + workshopSetId + "&active=true" + "&startingDtBegin=" + DateTime.Now.ToString(DateFormat);
            var response = await helpsClient.GetAsync("api/workshop/search?" + queryString);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Response<Workshop>>();
            }
            return ResponseHelper.CreateErrorResponse<Workshop>("Could not find workshops");
        }

        public async Task<Response<Workshop>> GetWorkshop(int workshopId)
        {
            return null;
        }
    }
}
