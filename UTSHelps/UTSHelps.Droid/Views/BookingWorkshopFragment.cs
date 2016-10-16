
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

namespace UTSHelps.Droid
{
	public class BookingWorkshopFragment : Fragment
	{
		private int workshopId;
		private String studentId;
		private Button bookWorkshopbtn;
		private Button addWaitListbtn;
		private Button removeWaitListbtn;
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
			workshopBookingProgressBar = view.FindViewById<ProgressBar>(Resource.Id.workshopBooking_progress);
			lnrWorkshopDetails = view.FindViewById<RelativeLayout>(Resource.Id.lnrBookingDetails);


			Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			addWaitListbtn = view.FindViewById<Button>(Resource.Id.workshopWaitlistBtn);
			removeWaitListbtn = view.FindViewById<Button>(Resource.Id.workshopRemoveWaitlistBtn);
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
			var places = workshop[0].cutoff - workshop[0].BookingCount;
			txtworkshopPlaces.Text = workshop[0].BookingCount + "/" + workshop[0].cutoff;
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
				removeWaitListbtn.Visibility = ViewStates.Visible;
			}
			else if (places <= 0)
			{
				addWaitListbtn.Visibility = ViewStates.Visible;
				bookWorkshopbtn.Visibility = ViewStates.Gone;
				removeWaitListbtn.Visibility = ViewStates.Gone;
			}
			else
			{
				bookWorkshopbtn.Visibility = ViewStates.Visible;
				addWaitListbtn.Visibility = ViewStates.Gone;
				removeWaitListbtn.Visibility = ViewStates.Gone;
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
				Toast.MakeText(this.Activity, "Booking is Success", ToastLength.Short).Show();
			}
			else
			{
				DialogHelper.ShowDialog(this.Activity, "Error", response.DisplayMessage);
			}
		}

		async void AddWaitListbtn_Click(object sender, EventArgs e)
		{
			var response = await ServiceHelper.Workshop.CreateWorkshopWaiting(workshopId, studentId);
			if (response.IsSuccess)
			{
				Toast.MakeText(this.Activity, "Added to Waitlist", ToastLength.Short).Show();
			}
			else
			{
				DialogHelper.ShowDialog(this.Activity, "Error", response.DisplayMessage);
			}
		}
	}
}
