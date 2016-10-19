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
using Android.Content.PM;
using Android.Graphics;
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
	[Activity(Label = "DashboardActivity",ScreenOrientation = ScreenOrientation.Portrait)]
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
        
        private TextView bookingPageTxt;
        private TextView addBookingPageTxt;
        private TextView settingsPageTxt;

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

            bookingPageTxt = FindViewById<TextView>(Resource.Id.txtbookings);
            addBookingPageTxt = FindViewById<TextView>(Resource.Id.txtAdd);
            settingsPageTxt = FindViewById<TextView>(Resource.Id.txtsettings);

            SetInitialMenuHighlight(startPage);

            bookingPage.Click += BookingPage_Click;
			addBookingPage.Click += AddBookingPage_Click;
			settingsPage.Click += SettingsPage_Click;
        }

        private void SetInitialMenuHighlight(string page)
        {
            switch (page)
            {
                case "MakeBooking":
                    ChangeMenuHighlight(addBookingPage, addBookingPageTxt);
                    break;
                case "Settings":
                    ChangeMenuHighlight(settingsPage, settingsPageTxt);
                    break;
                default:
                    ChangeMenuHighlight(bookingPage, bookingPageTxt);
                    break;
            }
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
            ChangeMenuHighlight(bookingPage, bookingPageTxt);
        }

        void AddBookingPage_Click(object sender, EventArgs e)
        {
            ReplaceFragment(makeBooking);
            ChangeMenuHighlight(addBookingPage, addBookingPageTxt);
        }

        void SettingsPage_Click(object sender, EventArgs e)
        {
            ReplaceFragment(settings);
            ChangeMenuHighlight(settingsPage, settingsPageTxt);
        }

        private void ChangeMenuHighlight(FontAwesome menuIcon, TextView menuText)
        {
            ResetMenuIcons();

            menuIcon.SetTextColor(Color.ParseColor("#FFFFFF"));
            menuText.SetTextColor(Color.ParseColor("#FFFFFF"));
        }

        private void ResetMenuIcons()
        {
            // Menu icons
            bookingPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            addBookingPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            settingsPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));

            // Menu text
            bookingPageTxt.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            addBookingPageTxt.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            settingsPageTxt.SetTextColor(Color.ParseColor("#B3FFFFFF"));
        }

        private void ReplaceFragment(Fragment selectedFragment)
        {
            var transaction = FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.mainFragmentContainer, selectedFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();

            currentFragment = selectedFragment;
        }

        #region FragmentButtons
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

        [Java.Interop.Export()]
        public void ShowSearch_Click(View view)
        {
            var searchItems = FindViewById<LinearLayout>(Resource.Id.searchDropdown);
            var searchIcon = FindViewById<FontAwesome>(Resource.Id.searchDropDownIcon);

            if (searchItems.Visibility == ViewStates.Visible)
            {
                searchIcon.Text = Resources.GetString(Resource.String.fa_angle_down);
                searchItems.Visibility = ViewStates.Gone;
            }
            else
            {
                searchIcon.Text = Resources.GetString(Resource.String.fa_angle_up);
                searchItems.Visibility = ViewStates.Visible;
            }
        }

        [Java.Interop.Export()]
        public void EditDate(View view)
        {
            DialogHelper.ShowDatePickerDialog(this, (TextView)view);
        }
        #endregion
    }
}