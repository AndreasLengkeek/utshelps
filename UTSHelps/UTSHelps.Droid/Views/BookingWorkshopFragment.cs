
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
using UTSHelps.Shared;
using Android.Provider;
using Java.Util;

namespace UTSHelps.Droid
{
	public class BookingWorkshopFragment : Fragment
	{
		private int workshopId;
		private String studentId;
		private Button bookWorkshopbtn;
		private Button addWaitListbtn;
		private List<Workshop> workshop = new List<Workshop>();
		private ProgressBar workshopBookingProgressBar;

		private TextView txtworkshopName;
		private TextView txtworkshopDate;
		private TextView txtworkshopTime;
		private TextView txtworkshopLocation;
		private TextView txtworkshopPlaces;
		private TextView txtworkshopSessionLocation;
		private TextView txtworkshopSessionTime;
		private TextView txtworkshopSessionDate;
		private TextView txtworkshopDesciption;
		private TextView txtworkshopSessionDay;
		private TextView txtWaitListed;
		private RelativeLayout lnrWorkshopDetails;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Bundle args = Arguments;
			workshopId = args.GetInt("workshopId");
			studentId = args.GetString("studentId");
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_BookingPage, container, false);

            this.Activity.ActionBar.Title = "Booking";

			Refresh(workshopId, studentId);

			txtworkshopName = view.FindViewById<TextView>(Resource.Id.workshopName);
			txtworkshopDate = view.FindViewById<TextView>(Resource.Id.workshopDate);
			txtworkshopTime = view.FindViewById<TextView>(Resource.Id.workshopTime);
			txtworkshopLocation = view.FindViewById<TextView>(Resource.Id.workshopLocation);
			txtworkshopDesciption = view.FindViewById<TextView>(Resource.Id.workshopDescription);
			txtworkshopPlaces = view.FindViewById<TextView>(Resource.Id.workshopPlaces);
			txtworkshopSessionLocation = view.FindViewById<TextView>(Resource.Id.workshopSessionLocation);
			txtworkshopSessionTime = view.FindViewById<TextView>(Resource.Id.workshopSessionTime);
			txtworkshopSessionDate = view.FindViewById<TextView>(Resource.Id.workshopSessionDate);
			txtworkshopSessionDay = view.FindViewById<TextView>(Resource.Id.workshopSessionDay);
			txtWaitListed = view.FindViewById<TextView>(Resource.Id.workshopWaitListedTxt);
			workshopBookingProgressBar = view.FindViewById<ProgressBar>(Resource.Id.workshopBooking_progress);
			lnrWorkshopDetails = view.FindViewById<RelativeLayout>(Resource.Id.lnrBookingDetails);


			//Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			addWaitListbtn = view.FindViewById<Button>(Resource.Id.workshopWaitlistBtn);
			addWaitListbtn.Click += AddWaitListbtn_Click;
			bookWorkshopbtn = view.FindViewById<Button>(Resource.Id.workshopBookingbtn);
			bookWorkshopbtn.Click += BookWorkshopbtn_Click;

			return view;
		}

		private void SetView(int workshopID, string studentID)
		{
			txtworkshopName.Text = workshop[0].topic;
			txtworkshopDate.Text = workshop[0].StartDate.ToShortDateString() + " - " + workshop[0].EndDate.ToShortDateString();
			txtworkshopTime.Text = workshop[0].StartDate.ToString("hh:mm") + " - " + workshop[0].EndDate.ToString("hh:mm");
			txtworkshopLocation.Text = workshop[0].campus;
			txtworkshopDesciption.Text = workshop[0].description;
			if (workshop[0].cutoff == null)
			{
				txtworkshopPlaces.Text = "Open";
			}
			else
			{
				txtworkshopPlaces.Text = workshop[0].BookingCount + "/" + workshop[0].cutoff;
			}
			var places = workshop[0].cutoff - workshop[0].BookingCount;
			txtworkshopSessionLocation.Text = workshop[0].campus;
			txtworkshopSessionTime.Text = workshop[0].StartDate.ToString("hhtt") + " - " + workshop[0].EndDate.ToString("hhtt");
			txtworkshopSessionDate.Text = workshop[0].StartDate.ToShortDateString();
			txtworkshopSessionDay.Text = workshop[0].StartDate.ToString("dddd");

			SetButtonView(places, workshopID, studentID);
		}

		private async void SetButtonView(int? places, int workshopID, string studentID)
		{
			var waitListResponse = await ServiceHelper.Workshop.IsWaitListed(workshopID, studentID);
			if (waitListResponse.IsWaitListed)
			{
				bookWorkshopbtn.Visibility = ViewStates.Gone;
				addWaitListbtn.Visibility = ViewStates.Gone;
				txtWaitListed.Visibility = ViewStates.Visible;
			}
			else if (places <= 0)
			{
				addWaitListbtn.Visibility = ViewStates.Visible;
				bookWorkshopbtn.Visibility = ViewStates.Gone;
				txtWaitListed.Visibility = ViewStates.Gone;
			}
			else
			{
				bookWorkshopbtn.Visibility = ViewStates.Visible;
				addWaitListbtn.Visibility = ViewStates.Gone;
				txtWaitListed.Visibility = ViewStates.Gone;
			}
		}

		private async void Refresh(int workshopID, string studentID)
		{
			var workshopResponse = await ServiceHelper.Workshop.GetWorkshop(workshopID);
			if (workshopResponse.IsSuccess)
			{
				workshop = workshopResponse.Results;
				if (workshop.Count == 0)
				{
					workshopBookingProgressBar.Visibility = ViewStates.Visible;
				}
				else
				{
					workshopBookingProgressBar.Visibility = ViewStates.Gone;
					SetView(workshopID,studentID);
					lnrWorkshopDetails.Visibility = ViewStates.Visible;
				}
			}
		}

		async void BookWorkshopbtn_Click(object sender, EventArgs e)
		{
			var response = await ServiceHelper.Workshop.CreateWorkshopBooking(workshopId, studentId);
			if (response.IsSuccess)
			{
				Toast.MakeText(this.Activity, "Booking Success!", ToastLength.Short).Show();
				SetReminder();
			}
			else
			{
				DialogHelper.ShowDialog(this.Activity, "Error", response.DisplayMessage);
			}
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
			builder.SetNegativeButton("No", (sender, args) => {
				GoToWorkshopBooked();
			});
			builder.Create().Show();
		}

		void GoToWorkshopBooked()
		{
			Bundle bundle = new Bundle();
			bundle.PutInt("workshopId", workshopId);
			bundle.PutString("studentId", studentId);

			BookedWorkshopFragment bookedWorkshop = new BookedWorkshopFragment();
			bookedWorkshop.Arguments = bundle;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bookedWorkshop, "BookedFragment");
			trans.Commit();
		}

		void SetReminderConfirmation()
		{
			var builder = new AlertDialog.Builder(this.Activity);
			LayoutInflater inflater = this.Activity.LayoutInflater;
			View view = inflater.Inflate(Resource.Layout.AlertDialog_notifications, null, false);
			builder.SetView(view);
			builder.SetTitle("Add Notification");
			builder.SetPositiveButton("OK", (sender, e) => {
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
				GoToWorkshopBooked();
				//add calendar function here
			});
			builder.SetNegativeButton("Cancel", (sender, e) => {
				GoToWorkshopBooked();
			});
			builder.Create().Show();
		}

		void SetCalendarMins()
		{
			ContentValues values = new ContentValues();
			values.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			values.Put(CalendarContract.Events.InterfaceConsts.Dtstart, (long)(workshop[0].StartDate.Date - new DateTime(1970,1,1)).TotalMilliseconds);
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

		async void AddWaitListbtn_Click(object sender, EventArgs e)
		{
			var waitListCountResponse = await ServiceHelper.Workshop.GetWaitListCount(workshopId);
			if (waitListCountResponse.IsSuccess)
			{
				var builder = new AlertDialog.Builder(this.Activity);
				builder.SetMessage("There is " + waitListCountResponse.Count + " in the wait list" );
				builder.SetTitle("Do you want to continue");
				builder.SetPositiveButton("Add to waitlist", (senderAlert, args) => {
					AddWaitList();
				});
				builder.SetNegativeButton("Cancel", (senderAlert, args) =>{
					
				});
				builder.Create().Show();
			}

		}

		private async void AddWaitList()
		{
			var response = await ServiceHelper.Workshop.CreateWorkshopWaiting(workshopId, studentId);
			if (response.IsSuccess)
			{
				Toast.MakeText(this.Activity, "Added to waitlist", ToastLength.Short).Show();
			}
			else
			{
				DialogHelper.ShowDialog(this.Activity, "Error", response.DisplayMessage);
			}
		}
	}
}
