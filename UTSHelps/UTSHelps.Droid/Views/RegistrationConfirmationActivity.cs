using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UTSHelps.Droid
{
	[Activity(Label = "RegistrationConfirmationActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class RegistrationConfirmationActivity : MainActivity
    {
        private string studentId;
        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Fragment_RegistrationConfirmation;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            studentId = Intent.GetStringExtra("studentId");
        }

        [Java.Interop.Export()]
        public void MakeBooking(View view)
        {
            var intent = new Intent(this, typeof(DashboardActivity));
            intent.PutExtra("StartScreen", "MakeBooking");
            intent.PutExtra("studentId", studentId);
            this.StartActivity(intent);
        }

        [Java.Interop.Export()]
        public void ContinueToApp(View view)
        {
            var intent = new Intent(this, typeof(DashboardActivity));
            intent.PutExtra("studentId", studentId);
            this.StartActivity(intent);
        }
    }
}