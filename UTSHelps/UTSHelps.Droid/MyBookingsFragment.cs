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
    public class MyBookingsFragment : Fragment
    {
		private List<string> mBookingWorkshops;
		private ListView bookingListView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyBookings, container, false);

			bookingListView = view.FindViewById<ListView>(Resource.Id.lstCurrentBooking);
			mBookingWorkshops = new List<string>();

			mBookingWorkshops.Add("UPASS: Testing");
			mBookingWorkshops.Add("Writing Workshop1");
			mBookingWorkshops.Add("YES!!!");

			BookingsAdapter adapter = new BookingsAdapter(this.Activity, mBookingWorkshops);

			bookingListView.Adapter = adapter;
            return view;
        }
    }
}