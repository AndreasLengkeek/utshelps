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

        private Toolbar toolbar;

        private MakeBookingFragment makeBooking;
        private MyBookingsFragment myBookings;
        private SettingsFragment settings;

        private FontAwesome bookingPage;
        private FontAwesome addBookingPage;
        private FontAwesome settingsPage;

        private Fragment currentFragment;

        private string subFragment;

        private TextView bookingPageTxt;
        private TextView addBookingPageTxt;
        private TextView settingsPageTxt;

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

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
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

            bookingPageTxt = FindViewById<TextView>(Resource.Id.txtbookings);
            addBookingPageTxt = FindViewById<TextView>(Resource.Id.txtAdd);
            settingsPageTxt = FindViewById<TextView>(Resource.Id.txtsettings);

            bookingPage = FindViewById<FontAwesome>(Resource.Id.action_mybookings);
            addBookingPage = FindViewById<FontAwesome>(Resource.Id.action_add);
            settingsPage = FindViewById<FontAwesome>(Resource.Id.action_settings);

            SetInitialMenuHighlight(startPage);

            bookingPage.Click += BookingPage_Click;
			addBookingPage.Click += AddBookingPage_Click;
			settingsPage.Click += SettingsPage_Click;
        }

        public void SetMenu(string fragmentName)
        {
            subFragment = fragmentName;
            this.SetActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (subFragment == "Booked")
            {
                MenuInflater.Inflate(Resource.Menu.booking_menu, menu);
            }
            else
            {
                MenuInflater.Inflate(Resource.Menu.blank_menu, menu);
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var title = item.TitleFormatted.ToString();
            if (title == "Add notes")
            {
                var dialogLayout = Resource.Layout.AlertDialog_AddNotes;
                var builder = DialogHelper.CreateCustomViewDialog(this, "Add a Note", LayoutInflater, dialogLayout);
                builder.SetPositiveButton("Save", AddNote);
                builder.SetNegativeButton("Cancel", (sender, e) => { });
                builder.Show();
            }
            else
            {
                Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                    ToastLength.Short).Show();
            }
            return base.OnOptionsItemSelected(item);
        }

        async void AddNote(object sender, DialogClickEventArgs e)
        {
            ISharedPreferences eprefs = this.GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
            var workshopId = eprefs.GetInt("workshopId", 0);
            var studentId = eprefs.GetString("studentId", null);

            var dialog = (AlertDialog)sender;
            var notes = dialog.FindViewById<EditText>(Resource.Id.txtAddNote);

            if (workshopId != 0 && studentId != null)
            {
                var response = await ServiceHelper.Workshop.AddNotes(workshopId, studentId, notes.Text);
                if (response.IsSuccess)
                {
                    Toast.MakeText(this, "Notes have been saved", ToastLength.Long).Show();
                }
                else
                {
                    DialogHelper.ShowDialog(this, "Cannot add notes", response.DisplayMessage);
                }
            } else
            {
                
                Toast.MakeText(this, "An unexpected error occured please try again later", ToastLength.Long).Show();
            }

            dialog.Dismiss();
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

        private void ChangeMenuHighlight(FontAwesome menuIcon, TextView menuText)
        {
            ResetMenuIcons();

            menuIcon.SetTextColor(Color.ParseColor("#FFFFFF"));
            menuText.SetTextColor(Color.ParseColor("#FFFFFF"));
        }

        private void ResetMenuIcons()
        {
            bookingPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            addBookingPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));
            settingsPage.SetTextColor(Color.ParseColor("#B3FFFFFF"));
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