using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UTSHelps.Shared.Services;

namespace UTSHelps.Droid.Helpers
{
    public static class ServiceHelper
    {
        public static WorkshopService Workshop;

        static ServiceHelper()
        {
            Workshop = new WorkshopService();
            Task.Factory.StartNew(HelpsService.Purge);
        }
    }
}