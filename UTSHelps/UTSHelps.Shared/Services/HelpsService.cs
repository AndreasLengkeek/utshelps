using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UTSHelps.Shared.Interfaces;
using Xamarin.Forms;

namespace UTSHelps.Shared.Services
{
    /// <summary>
    /// HelpsService defines the Helps API Client
    /// </summary>
    public class HelpsService
    {
        protected readonly static HttpClient helpsClient;
        private const string url = "http://utshelps.azurewebsites.net";
        private const string applicationKey = "123456";
        protected const string DateFormat = "yyy-MM-ddThh:mm";

        /// <summary>
        /// Initialse HelpsService
        /// </summary>
        static HelpsService()
        {
            helpsClient = new HttpClient();
            helpsClient.BaseAddress = new Uri(url);
            helpsClient.DefaultRequestHeaders.Add("AppKey", applicationKey);
            helpsClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Send an initial request to the helps server to wake it up
        /// </summary>
        public static async void Purge()
        {
            await helpsClient.GetAsync("api/sessionId/workshopSets/as");
        }

        /// <summary>
        /// Test the internet connection. Throws System.Net.WebException
        /// </summary>
        protected static void TestConnection()
        {
            //var networkConnection = DependencyService.Get<INetworkConnection>();
            //networkConnection.CheckNetworkConnection();
            //if (!networkConnection.IsConnected)
            //    throw new System.Net.WebException();
        }

        /// <summary>
        /// Helper method to define if the user has connection to the internet
        /// </summary>
        /// <returns></returns>
        protected static bool IsConnected()
        {
            //var networkConnection = DependencyService.Get<INetworkConnection>();
            //networkConnection.CheckNetworkConnection();
            //return networkConnection.IsConnected;

            return true;
        }
    }
}
