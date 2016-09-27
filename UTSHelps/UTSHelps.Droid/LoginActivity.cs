using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace UTSHelps.Droid
{
    [Activity(Label = "UTSHelps.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : MainActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.Main; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // TODO:
            //if (CurrentUser != null)
            //{
            //    SwitchActivity();
            //}
        }

        [Java.Interop.Export()]
        public void Login(View view)
        {
            var studentId = FindViewById<EditText>(Resource.Id.loginStudentId).Text;
            var password = FindViewById<EditText>(Resource.Id.loginPassword).Text;

            if (studentId != null && password != null)
            {
                SwitchActivity();
            }
        }

        private void SwitchActivity()
        {
            // TODO:
            // user hasn't logged in before, go to reg.
            // user is logged in, go to main page.
        }
    }
}

