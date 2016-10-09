using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace UTSHelps.Droid
{
    public class SettingsFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Settings, container, false);

            Button logout = view.FindViewById<Button>(Resource.Id.settingsLogout);
            logout.Click += Logout_Click;

            return view;
        }

        public void Logout_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this.Activity, typeof(LoginActivity));
            this.StartActivity(intent);
        }
    }
}