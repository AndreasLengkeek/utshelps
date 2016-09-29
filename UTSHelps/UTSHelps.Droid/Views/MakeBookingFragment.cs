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

namespace UTSHelps.Droid
{
    public class MakeBookingFragment : Fragment
    {
		private List<string> workshops;
		private ListView workshopListView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MakeBooking, container, false);
			workshops = new List<string>();

			workshops.Add("Test");
			workshops.Add("Writings");
			workshops.Add("Readings");

			workshopListView = view.FindViewById<ListView>(Resource.Id.lstWorkshop);

			WorkshopAdapter adapter = new WorkshopAdapter(this.Activity, workshops);

			workshopListView.Adapter = adapter;
            return view;
        }
    }
}