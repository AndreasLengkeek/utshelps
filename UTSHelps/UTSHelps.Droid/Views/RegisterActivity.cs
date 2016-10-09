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
        private int currentFragment;

        private RegistrationStudentInfoFragment introFragment;
        private List<Fragment> views;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_Registration;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            views = new List<Fragment> {
                new RegistrationStudentInfoFragment(),
                new RegistrationCourseFragment(),
                new RegistrationBackgroundFragment()
            };

            currentFragment = 0;

            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.regFragmentContainer, views[currentFragment]);
            transaction.Commit();
        }

        [Java.Interop.Export()]
        public void PrevScreen(View view)
        {
            if (currentFragment == 0)
            {
                return;
            }
            ReplaceFragment(--currentFragment);
        }

        [Java.Interop.Export()]
        public void NextScreen(View view)
        {
            if (currentFragment == 2)
            {
                FinishRegister();
            }
            ReplaceFragment(++currentFragment);
        }

        private void ReplaceFragment(int selected)
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.regFragmentContainer, views[selected]);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        private void FinishRegister()
        {
            throw new NotImplementedException();
        }
    }
}