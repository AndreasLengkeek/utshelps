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

            currentFragment = makeBooking;

            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.fragmentContainer, makeBooking, "MakeBooking_Fragment");
            transaction.Add(Resource.Id.fragmentContainer, myBookings, "MyBookings_Fragment");
            transaction.Add(Resource.Id.fragmentContainer, settings, "Settings_Fragment");

            transaction.Hide(myBookings);
            transaction.Hide(settings);

            transaction.Commit();

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
                case "My Bookings": ShowFragment(myBookings);
                    break;
                case "Add Booking": ShowFragment(makeBooking);
                    break;
                case "Settings": ShowFragment(settings);
                    break;
                default:
                    Toast.MakeText(this, "Something fucked up!", ToastLength.Short).Show();
                    break;
            }
            Toast.MakeText(this, "Here's Johnny (" + title + ")!", ToastLength.Short).Show();
        }

        private void ShowFragment(Fragment selectedFragment)
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Hide(currentFragment);
            transaction.Show(selectedFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();


            currentFragment = selectedFragment;
        }
    }
}