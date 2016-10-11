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
    public class MyBookingsFragment : Fragment
    {
		private List<Booking> mBookingWorkshops = new List<Booking>();
		private ListView bookingListView;
		private BookingsAdapter adapter;
		private string studentId;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			studentId = Arguments.GetString("studentId");
			//mBookingWorkshops.Add(new Booking { topic = "UPASS:TEST", starting = new DateTime(2012, 10, 11, 10, 30, 00), ending = new DateTime(2012, 10, 11, 12, 00, 00), campusID = 1 });
			//mBookingWorkshops.Add(new Booking { topic = "Academic Test for 1st year nursing", starting = new DateTime(2013, 12, 8, 12, 30, 00), ending = new DateTime(2013, 12, 8, 14, 00, 00), campusID = 2 });
			//mBookingWorkshops.Add(new Booking { topic = "Writing clinic 4", starting = new DateTime(2014, 1, 7, 9, 0, 00), ending = new DateTime(2014, 1, 7, 10, 00, 00), campusID = 3 });

			Refresh(studentId);
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyBookings, container, false);

			bookingListView = view.FindViewById<ListView>(Resource.Id.lstCurrentBooking);

			adapter = new BookingsAdapter(this.Activity, mBookingWorkshops);

			bookingListView.Adapter = adapter;
            return view;
        }

		private async void Refresh(string studentId)
		{
			var response = await ServiceHelper.Booking.GetBookings(studentId);
			if (response.IsSuccess)
			{
				mBookingWorkshops = response.Results;
				adapter.SwapItems(mBookingWorkshops);
			}
		}
    }
}