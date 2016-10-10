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
        private string studentId;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Fragment_RegistrationIntro;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var startButton = FindViewById<Button>(Resource.Id.rego1StartButton);
            studentId = Intent.GetStringExtra("studentId");
        }

        [Java.Interop.Export()]
        public void StartRegistration(View view)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            intent.PutExtra("studentId", studentId);
            this.StartActivity(intent);
        }
    }
}