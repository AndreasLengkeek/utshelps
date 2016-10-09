
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
		private String workshopName;
		private String workshopTime;
		private String workshopDate;
		private String workshopDescription;
		private String workshopLocation;
		private String workshopPlaces;
		private Button bookWorkshopbtn;
		//private List<Workshop> workshop = new List<Workshop>();

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Bundle args = Arguments;
			workshopId = args.GetInt("workshopId");
			workshopName = args.GetString("workshopName");
			workshopDescription = args.GetString("workshopDescription");
			workshopDate = args.GetString("workshopDate");
			workshopTime = args.GetString("workshopTime");
			workshopLocation = args.GetString("workshopLocation");
			workshopPlaces = args.GetString("workshopPlace");
			//Refresh(workshopId);
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_BookingPage, container, false);

			TextView txtworkshopName = view.FindViewById<TextView>(Resource.Id.workshopName);
			txtworkshopName.Text = workshopName;

			TextView txtworkshopDate = view.FindViewById<TextView>(Resource.Id.workshopDate);
			txtworkshopDate.Text = workshopDate;

			TextView txtworkshopTime = view.FindViewById<TextView>(Resource.Id.workshopTime);
			txtworkshopTime.Text = workshopTime;

			TextView txtworkshopLocation = view.FindViewById<TextView>(Resource.Id.workshopLocation);
			txtworkshopLocation.Text = workshopLocation;

			TextView txtworkshopDesciption = view.FindViewById<TextView>(Resource.Id.workshopDescription);
			txtworkshopDesciption.Text = workshopDescription;

			TextView txtworkshopPlaces = view.FindViewById<TextView>(Resource.Id.workshopPlaces);
			txtworkshopPlaces.Text = workshopPlaces;

			TextView txtworkshopSessionLocation = view.FindViewById<TextView>(Resource.Id.workshopSessionLocation);
			txtworkshopSessionLocation.Text = workshopLocation;

			TextView txtworkshopSessionTime = view.FindViewById<TextView>(Resource.Id.workshopSessionTime);
			txtworkshopSessionTime.Text = workshopTime;

			TextView txtworkshopSessionDate = view.FindViewById<TextView>(Resource.Id.workshopSessionDate);
			txtworkshopSessionDate.Text = workshopDate;

			Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			bookWorkshopbtn = view.FindViewById<Button>(Resource.Id.workshopBookingbtn);
			bookWorkshopbtn.Click += BookWorkshopbtn_Click;

			return view;
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		}

		//private async void Refresh(int workshopID)
		//{
		//	var response = await ServiceHelper.Workshop.GetWorkshop(workshopID);
		//	if (response.IsSuccess)
		//	{
		//		workshop = response.Results;
		//	}
		//}

		void BookWorkshopbtn_Click(object sender, EventArgs e)
		{

		}
	}
}
