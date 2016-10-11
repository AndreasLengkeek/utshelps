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
using System.Threading.Tasks;

namespace UTSHelps.Droid.Helpers
{
    public static class ServiceHelper
    {
        public static WorkshopService Workshop;
        public static StudentService Student;
        public static BookingService Booking;
        public static MiscService Misc;

        static ServiceHelper()
        {
            Workshop = new WorkshopService();
            Student = new StudentService();
            Booking = new BookingService();
            Misc = new MiscService();
            Task.Factory.StartNew(HelpsService.Purge);
        }
    }
}