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
    [Activity(Label = "RegistrationIntroActivity")]
    public class RegistrationIntroActivity : MainActivity
    {
        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Fragment_Registration1;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var startButton = FindViewById<Button>(Resource.Id.rego1StartButton);
        }

        [Java.Interop.Export()]
        public void StartRegistration(View view)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            this.StartActivity(intent);
        }
    }
}