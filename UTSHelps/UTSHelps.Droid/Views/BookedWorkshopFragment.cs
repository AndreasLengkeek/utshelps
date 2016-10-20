
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using UTSHelps.Droid.Helpers;
using System.Threading.Tasks;
using UTSHelps.Shared.Models;
using Android.Provider;

namespace UTSHelps.Droid
{
	public class BookedWorkshopFragment : Fragment
	{
		private Button cancelButton;
		private String studentId;
		private int workshopId;
		private List<Workshop> workshop;
		private ProgressBar workshopBookedProgressBar;

		private TextView txtBookedworkshopName;
		private TextView txtBookedworkshopDate;
		private TextView txtBookedworkshopTime;
		private TextView txtBookedworkshopLocation;
		private TextView txtBookedworkshopPlaces;
		private TextView txtBookedworkshopSessionLocation;
		private TextView txtBookedworkshopSessionTime;
		private TextView txtBookedworkshopSessionDate;
		private TextView txtBookedworkshopDesciption;
		private TextView txtBookedworkshopSessionDay;
		private RelativeLayout lnrBookedWorkshopDetails;
		private Button addReminder;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			workshopId = Arguments.GetInt("workshopId");
			studentId = Arguments.GetString("studentId");
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_BookedWorkshop, container, false);
			this.Activity.ActionBar.Title = "Booking";

            ISharedPreferences eprefs = Context.GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
            var editor = eprefs.Edit();
            editor.PutInt("workshopId", workshopId);
            editor.PutString("studentId", studentId);
            editor.Commit();

            var dashboard = (DashboardActivity)this.Activity;
            dashboard.SetMenu("Booked");

            Refresh(workshopId);

			txtBookedworkshopName = view.FindViewById<TextView>(Resource.Id.workshopBookedName);
			txtBookedworkshopDate = view.FindViewById<TextView>(Resource.Id.workshopBookedDate);
			txtBookedworkshopTime = view.FindViewById<TextView>(Resource.Id.workshopBookedTime);
			txtBookedworkshopLocation = view.FindViewById<TextView>(Resource.Id.workshopBookedLocation);
			txtBookedworkshopDesciption = view.FindViewById<TextView>(Resource.Id.workshopBookedDescription);
			txtBookedworkshopPlaces = view.FindViewById<TextView>(Resource.Id.workshopBookedPlaces);
			txtBookedworkshopSessionLocation = view.FindViewById<TextView>(Resource.Id.workshopBookedSessionLocation);
			txtBookedworkshopSessionTime = view.FindViewById<TextView>(Resource.Id.workshopBookedSessionTime);
			txtBookedworkshopSessionDate = view.FindViewById<TextView>(Resource.Id.workshopBookedSessionDate);
			txtBookedworkshopSessionDay = view.FindViewById<TextView>(Resource.Id.workshopBookedSessionDay);
			lnrBookedWorkshopDetails = view.FindViewById<RelativeLayout>(Resource.Id.lnrBookedDetails);
			workshopBookedProgressBar = view.FindViewById<ProgressBar>(Resource.Id.workshopBooked_progress);
			addReminder = view.FindViewById<Button>(Resource.Id.addReminderBtn);

			//Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			addReminder.Click += AddReminder_Click;

			cancelButton = view.FindViewById<Button>(Resource.Id.workshopCancelbtn);
			cancelButton.Click += CancelButton_Click;

			return view;
		}

		void AddReminder_Click(object sender, EventArgs e)
		{
			SetReminder();
		}

		void SetReminder()
		{
			var builder = new AlertDialog.Builder(this.Activity);
			builder.SetTitle("Booking Confirmed!");
			builder.SetMessage("Would you like to set a reminder?");
			builder.SetPositiveButton("Yes", (sender, args) =>
			{
				SetReminderConfirmation();
			});
			builder.SetNegativeButton("No", (sender, args) =>
			{
				
			});
			builder.Create().Show();
		}

		void SetReminderConfirmation()
		{
			var builder = new AlertDialog.Builder(this.Activity);
			LayoutInflater inflater = this.Activity.LayoutInflater;
			View view = inflater.Inflate(Resource.Layout.AlertDialog_notifications, null, false);
			builder.SetView(view);
			builder.SetTitle("Add Notification");
			builder.SetPositiveButton("OK", (sender, e) =>
			{
				RadioButton mins30radio = view.FindViewById<RadioButton>(Resource.Id.Minute30Radio);
				RadioButton day1radio = view.FindViewById<RadioButton>(Resource.Id.Day1Radio);
				RadioButton week1radio = view.FindViewById<RadioButton>(Resource.Id.Week1Radio);
				if (mins30radio.Checked)
				{
					SetCalendarMins();
				}
				else if (day1radio.Checked)
				{
					SetCalendarDay();
				}
				else if (week1radio.Checked)
				{
					SetCalendarWeek();
				}

				//add calendar function here
			});
			builder.SetNegativeButton("Cancel", (sender, e) =>
			{
				
			});
			builder.Create().Show();
		}

		void SetCalendarMins()
		{
			ContentValues values = new ContentValues();
			values.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtstart, (long)(workshop[0].StartDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtend, (long)(workshop[0].EndDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.Title, workshop[0].topic);
			values.Put(CalendarContract.Events.InterfaceConsts.Description, workshop[0].description);
			values.Put(CalendarContract.Events.InterfaceConsts.EventLocation, workshop[0].campus);
			var calendar = this.Activity.ContentResolver.Insert(CalendarContract.Events.ContentUri, values);
			var eventID = long.Parse(calendar.LastPathSegment);

			ContentValues reminderValues = new ContentValues();
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, 30);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Method, 1);
			var reminder = this.Activity.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, reminderValues);
		}

		void SetCalendarDay()
		{
			ContentValues values = new ContentValues();
			values.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtstart, (long)(workshop[0].StartDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtend, (long)(workshop[0].EndDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.Title, workshop[0].topic);
			values.Put(CalendarContract.Events.InterfaceConsts.Description, workshop[0].description);
			values.Put(CalendarContract.Events.InterfaceConsts.EventLocation, workshop[0].campus);
			var calendar = this.Activity.ContentResolver.Insert(CalendarContract.Events.ContentUri, values);
			var eventID = long.Parse(calendar.LastPathSegment);

			ContentValues reminderValues = new ContentValues();
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, 1440);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Method, 1);
			var reminder = this.Activity.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, reminderValues);
		}

		void SetCalendarWeek()
		{
			ContentValues values = new ContentValues();
			values.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtstart, (long)(workshop[0].StartDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtend, (long)(workshop[0].EndDate.Date - new DateTime(1970, 1, 1)).TotalMilliseconds);
			values.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
			values.Put(CalendarContract.Events.InterfaceConsts.Title, workshop[0].topic);
			values.Put(CalendarContract.Events.InterfaceConsts.Description, workshop[0].description);
			values.Put(CalendarContract.Events.InterfaceConsts.EventLocation, workshop[0].campus);
			var calendar = this.Activity.ContentResolver.Insert(CalendarContract.Events.ContentUri, values);
			var eventID = long.Parse(calendar.LastPathSegment);

			ContentValues reminderValues = new ContentValues();
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, 10080);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
			reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Method, 1);
			var reminder = this.Activity.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, reminderValues);
		}

		public override void OnDestroyView()
        {
            var dashboard = (DashboardActivity)this.Activity;
            dashboard.SetMenu("");
            base.OnDestroyView();
        }

        private void SetView()
		{
			txtBookedworkshopName.Text = workshop[0].topic;
			txtBookedworkshopDate.Text = workshop[0].StartDate.ToShortDateString() + " - " + workshop[0].EndDate.ToShortDateString();
			txtBookedworkshopTime.Text = workshop[0].StartDate.ToString("hh:mm") + " - " + workshop[0].EndDate.ToString("hh:mm");
			txtBookedworkshopLocation.Text = workshop[0].campus;
			txtBookedworkshopDesciption.Text = workshop[0].description;
			if (workshop[0].cutoff == null)
			{
				txtBookedworkshopPlaces.Text = "Open";
			}
			else
			{ 
				txtBookedworkshopPlaces.Text = workshop[0].BookingCount + "/" + workshop[0].cutoff;
			}
			txtBookedworkshopSessionLocation.Text = workshop[0].campus;
			txtBookedworkshopSessionTime.Text = workshop[0].StartDate.ToString("hhtt") + " - " + workshop[0].EndDate.ToString("hhtt");
			txtBookedworkshopSessionDate.Text = workshop[0].StartDate.ToShortDateString();
			txtBookedworkshopSessionDay.Text = workshop[0].StartDate.ToString("dddd");
		}

		async void Refresh(int workshopId)
		{
			var response = await ServiceHelper.Workshop.GetWorkshop(workshopId);
			if (response.IsSuccess)
			{
				workshop = response.Results;
				if (workshop.Count == 0)
				{
					workshopBookedProgressBar.Visibility = ViewStates.Visible;
				}
				else
				{
					workshopBookedProgressBar.Visibility = ViewStates.Gone;
					SetView();
					lnrBookedWorkshopDetails.Visibility = ViewStates.Visible;
				}
			}
		}

		async void CancelButton_Click(object sender, EventArgs e)
		{
			var response = await ServiceHelper.Workshop.CancelWorkshopBooking(workshopId, studentId);
			if (response.IsSuccess)
			{
				Toast.MakeText(this.Activity, "Cancel successful", ToastLength.Short).Show();

            	var builder = new AlertDialog.Builder(this.Activity);
                builder.SetTitle("Cancellation Confirmed!");
				builder.SetMessage("You have successfully cancelled this booking");
				builder.SetPositiveButton("OK", (senderAlert, args) => {
					Bundle bundle = new Bundle();
					bundle.PutString("studentId", studentId);
					MyBookingsFragment myBooking = new MyBookingsFragment();
					myBooking.Arguments = bundle;
					ChangePage(myBooking);
				});
				builder.Create().Show();
			}
			else
			{
				DialogHelper.ShowDialog(this.Activity, "Error", response.DisplayMessage);
			}
		}

		void ChangePage(Fragment fragment)
		{
			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, fragment, "MyBooking");
			trans.Commit();
		}
	}
}
