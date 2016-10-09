
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

namespace UTSHelps.Droid
{
	public class BookingWorkshopFragment : Fragment
	{
		private int workshopId;
		private String workshopName;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Bundle args = Arguments;
			workshopId = args.GetInt("workshopId");
			workshopName = args.GetString("workshopName");
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_BookingPage, container, false);

			TextView txtworkshopName = view.FindViewById<TextView>(Resource.Id.workshopName);
			txtworkshopName.Text = workshopName;
			Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			return view;
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		}
	}
}
