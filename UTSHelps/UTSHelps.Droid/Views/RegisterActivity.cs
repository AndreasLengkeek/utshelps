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
using UTSHelps.Droid.Helpers;
using Android.Graphics;
using UTSHelps.Shared.Models;
using UTSHelps.Droid.ViewModels;

namespace UTSHelps.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : MainActivity
    {
        private int currentFragment;
        private List<Fragment> views;

        private RegistrationStudentInfoViewModel studentInfo;

        private FontAwesome leftArrow;
        private FontAwesome rightArrow;
        private List<FontAwesome> pagerCircles;

        private string fullCircle;
        private string openCircle;

        private string studentId;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_Registration;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitComponents();

            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.regFragmentContainer, views[currentFragment]);
            transaction.Commit();



            studentId = Intent.GetStringExtra("studentId");
        }

        private void InitComponents()
        {

            views = new List<Fragment> {
                new RegistrationStudentInfoFragment(),
                new RegistrationCourseFragment(),
                new RegistrationBackgroundFragment()
            };

            currentFragment = 0;

            leftArrow = FindViewById<FontAwesome>(Resource.Id.regLeftArrow);
            rightArrow = FindViewById<FontAwesome>(Resource.Id.regRightArrow);

            pagerCircles = new List<FontAwesome> {
                FindViewById<FontAwesome>(Resource.Id.regCircle1),
                FindViewById<FontAwesome>(Resource.Id.regCircle2),
                FindViewById<FontAwesome>(Resource.Id.regCircle3)
            };

            fullCircle = Resources.GetString(Resource.String.fa_circle);
            openCircle = Resources.GetString(Resource.String.fa_circle_thin);
        }

        [Java.Interop.Export()]
        public void PrevScreen(View view)
        {
            if (currentFragment == 0)
            {
                return;
            }
            ReplaceFragment(--currentFragment);
            UpdatePager();
        }

        [Java.Interop.Export()]
        public void NextScreen(View view)
        {
            GetDataFromFragment();
            if (currentFragment == 2)
            {
                FinishRegister();
            }
            else
            {
                ReplaceFragment(++currentFragment);
                UpdatePager();
            }
        }

        private void GetDataFromFragment()
        {
            switch (currentFragment)
            {
                case 0:
                    studentInfo = ((RegistrationStudentInfoFragment)views[currentFragment]).GetData();
                    break;
                case 1:
                    //courseInfo = ((RegistrationStudentInfoFragment)views[currentFragment]).GetData();
                    break;
                default:
                    //background = ((RegistrationStudentInfoFragment)views[currentFragment]).GetData();
                    break;
            }
        }

        private Student ParseStudentFromFragments()
        {
            return new Student {
                preferred_name = studentInfo.PrefName,
                alternative_contact = studentInfo.AltContact,
                country_origin = studentInfo.Country,
                first_language = studentInfo.Language,
                gender = studentInfo.Gender.ToString(),
                status = studentInfo.Status.ToString(),
                dob = studentInfo.BirthDate
            };
        }

        private void ReplaceFragment(int selected)
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.regFragmentContainer, views[selected]);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        private void UpdatePager()
        {
            // change indicator for current page
            for (int i = 0; i < pagerCircles.Count; i++)
            {
                pagerCircles[i].Text = (i == currentFragment ? fullCircle : openCircle);
            }

            ResetPagerArrows();

            // update arrows on first and last page
            if (currentFragment == 0)
            {
                leftArrow.SetTextColor(Color.ParseColor("#80FFFFFF"));
            }
            else if (currentFragment == 2)
            {
                rightArrow.Text = Resources.GetString(Resource.String.fa_check);
            }
        }

        private void ResetPagerArrows()
        {
            leftArrow.SetTextColor(Color.ParseColor("#FFFFFF"));
            rightArrow.SetTextColor(Color.ParseColor("#FFFFFF"));
            leftArrow.Text = Resources.GetString(Resource.String.fa_angle_left);
            rightArrow.Text = Resources.GetString(Resource.String.fa_angle_right);
        }

        private void FinishRegister()
        {
            var newStudent = ParseStudentFromFragments();
            // TODO: Register student with api
            // TODO: Redirect to final register page

            Toast.MakeText(this, "Registering... (Not yet)", ToastLength.Short).Show();

            var intent = new Intent(this, typeof(RegistrationConfirmationActivity));
            this.StartActivity(intent);
        }
    }
}