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

            var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
            if (editToolbar != null)
            {
                editToolbar.InflateMenu(Resource.Menu.edit_menus);
                editToolbar.MenuItemClick += MenuSelect;
            }
        }

        private void MenuSelect(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            var title = e.Item.TitleFormatted.ToString();
            switch (title)
            {
				case "My Bookings": ReplaceFragment(myBookings);
                    break;
				case "Add Booking": ReplaceFragment(makeBooking);
                    break;
				case "Settings": ReplaceFragment(settings);
                    break;
                default:
                    Toast.MakeText(this, "Something fucked up!", ToastLength.Short).Show();
                    break;
            }
            Toast.MakeText(this, "Here's Johnny (" + title + ")!", ToastLength.Short).Show();
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