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
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : MainActivity
    {
		private string studentId;
		private Bundle studentIdBundle;

        private MakeBookingFragment makeBooking;
        private MyBookingsFragment myBookings;
        private SettingsFragment settings;

        private FontAwesome bookingPage;
        private FontAwesome addBookingPage;
        private FontAwesome settingsPage;

        private Fragment currentFragment;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_Dashboard;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			studentId = Intent.GetStringExtra("studentId");
			studentIdBundle = new Bundle();
			studentIdBundle.PutString("studentId", studentId);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            makeBooking = new MakeBookingFragment();
            myBookings = new MyBookingsFragment();
            settings = new SettingsFragment();

			makeBooking.Arguments = studentIdBundle;
			myBookings.Arguments = studentIdBundle;

            var startPage = Intent.GetStringExtra("StartScreen");
			currentFragment = GetStartScreen(startPage);

            var transaction = FragmentManager.BeginTransaction();
			transaction.Add(Resource.Id.mainFragmentContainer, currentFragment, "MakeBooking_Fragment");
            transaction.Commit();

            bookingPage = FindViewById<FontAwesome>(Resource.Id.action_mybookings);
            addBookingPage = FindViewById<FontAwesome>(Resource.Id.action_add);
            settingsPage = FindViewById<FontAwesome>(Resource.Id.action_settings);

            bookingPage.Click += BookingPage_Click;
			addBookingPage.Click += AddBookingPage_Click;
			settingsPage.Click += SettingsPage_Click;
        }

        private Fragment GetStartScreen(string page)
        {
            switch (page)
            {
                case "MakeBooking":
                    return makeBooking;
                case "Settings":
                    return settings;
                default:
                    return myBookings;
            }
        }

        void BookingPage_Click(object sender, EventArgs e)
		{
            ReplaceFragment(myBookings);
        }

		void AddBookingPage_Click(object sender, EventArgs e)
		{
			ReplaceFragment(makeBooking);
        }

		void SettingsPage_Click(object sender, EventArgs e)
		{
			ReplaceFragment(settings);
        }

        private void ReplaceFragment(Fragment selectedFragment)
        {
            var transaction = FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.mainFragmentContainer, selectedFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();

            currentFragment = selectedFragment;
        }

        [Java.Interop.Export()]
        public void Search_Click(View view)
        {
            DialogHelper.ShowDialog(this, "Search", "Search Options!");
        }

        [Java.Interop.Export()]
        public void Details_Click(View view)
        {
            var intent = new Intent(this, typeof(StudentDetailsActivity));
            this.StartActivity(intent);
        }

        [Java.Interop.Export()]
        public void Logout_Click(View view)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            this.StartActivity(intent);
        }
    }
}