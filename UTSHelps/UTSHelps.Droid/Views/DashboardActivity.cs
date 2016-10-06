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

            var transaction = FragmentManager.BeginTransaction();
			transaction.Add(Resource.Id.fragmentContainer, myBookings, "MakeBooking_Fragment");

            transaction.Commit();
			currentFragment = myBookings;

			bookingPage = FindViewById<LinearLayout>(Resource.Id.linearbooking);
			addBookingPage = FindViewById<LinearLayout>(Resource.Id.linearadd);
			settingsPage = FindViewById<LinearLayout>(Resource.Id.linearsettings);

			bookingPage.Click += BookingPage_Click;
			addBookingPage.Click += AddBookingPage_Click;
			settingsPage.Click += SettingsPage_Click;
        }

		void BookingPage_Click(object sender, EventArgs e)
		{
			ReplaceFragment(myBookings);
			Toast.MakeText(this, "Here's Johnny ( myBooking )!", ToastLength.Short).Show();
		}

		void AddBookingPage_Click(object sender, EventArgs e)
		{
			ReplaceFragment(makeBooking);
			Toast.MakeText(this, "Here's Johnny ( addBooking )!", ToastLength.Short).Show();
		}

		void SettingsPage_Click(object sender, EventArgs e)
		{
			ReplaceFragment(settings);
			Toast.MakeText(this, "Here's Johnny ( settings )!", ToastLength.Short).Show();
		}

        private void ReplaceFragment(Fragment selectedFragment)
        {
            var transaction = FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.fragmentContainer, selectedFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();


            currentFragment = selectedFragment;
        }
    }
}