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

namespace UTSHelps.Droid
{
    [Activity(Label = "MakeBookingActivity")]
    public class MakeBookingActivity : MainActivity
    {
        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_MakeBooking;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }
    }
}