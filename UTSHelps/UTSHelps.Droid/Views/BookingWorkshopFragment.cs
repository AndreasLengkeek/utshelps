
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

namespace UTSHelps.Droid
{
	public class BookingWorkshopFragment : Fragment
	{
		private int workshopId;
		private String studentId;
		private Button bookWorkshopbtn;
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

			//Refresh(workshopId);
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_BookingPage, container, false);

            this.Activity.ActionBar.Title = "Workshop";

			Refresh(workshopId);

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

			bookWorkshopbtn = view.FindViewById<Button>(Resource.Id.workshopBookingbtn);
			bookWorkshopbtn.Click += BookWorkshopbtn_Click;

			return view;
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		}

		private void SetView()
		{
			txtworkshopName.Text = workshop[0].topic;
			txtworkshopDate.Text = workshop[0].StartDate.ToShortDateString() + " - " + workshop[0].EndDate.ToShortDateString();
			txtworkshopTime.Text = workshop[0].StartDate.ToString("hh:mm") + " - " + workshop[0].EndDate.ToString("hh:mm");
			txtworkshopLocation.Text = workshop[0].campus;
			txtworkshopDesciption.Text = workshop[0].description;
			txtworkshopPlaces.Text = workshop[0].BookingCount + "/" + workshop[0].cutoff;
			txtworkshopSessionLocation.Text = workshop[0].campus;
			txtworkshopSessionTime.Text = workshop[0].StartDate.ToString("hhtt") + " - " + workshop[0].EndDate.ToString("hhtt");
			txtworkshopSessionDate.Text = workshop[0].StartDate.ToShortDateString();
			txtworkshopSessionDay.Text = workshop[0].StartDate.ToString("dddd");
		}

		private async void Refresh(int workshopID)
		{
			var response = await ServiceHelper.Workshop.GetWorkshop(workshopID);
			if (response.IsSuccess)
			{
				workshop = response.Results;
				if (workshop.Count == 0)
				{
					workshopBookingProgressBar.Visibility = ViewStates.Visible;
				}
				else
				{
					workshopBookingProgressBar.Visibility = ViewStates.Gone;
					SetView();
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
				Toast.MakeText(this.Activity, "Booking has Fail", ToastLength.Short).Show();
			}
		}
	}
}
