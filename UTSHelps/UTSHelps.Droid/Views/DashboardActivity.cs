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
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : MainActivity
    {
        private MakeBookingFragment makeBooking;
        private MyBookingsFragment myBookings;
        private SettingsFragment settings;

		private LinearLayout bookingPage;
		private LinearLayout addBookingPage;
		private LinearLayout settingsPage;

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

            makeBooking = new MakeBookingFragment();
            myBookings = new MyBookingsFragment();
            settings = new SettingsFragment();

            var startPage = Intent.GetStringExtra("StartScreen");
			currentFragment = GetStartScreen(startPage);

            var transaction = FragmentManager.BeginTransaction();
			transaction.Add(Resource.Id.mainFragmentContainer, currentFragment, "MakeBooking_Fragment");
            transaction.Commit();

			bookingPage = FindViewById<LinearLayout>(Resource.Id.linearbooking);
			addBookingPage = FindViewById<LinearLayout>(Resource.Id.linearadd);
			settingsPage = FindViewById<LinearLayout>(Resource.Id.linearsettings);

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
    }
}