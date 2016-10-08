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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : MainActivity
    {
        private Fragment currentFragment;

        private RegistrationIntroFragment introFragment;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_Registration;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            introFragment = new RegistrationIntroFragment();

            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.regFragmentContainer, introFragment, "RegistrationIntro_Fragment");
            transaction.Commit();

            currentFragment = introFragment;
        }
    }
}