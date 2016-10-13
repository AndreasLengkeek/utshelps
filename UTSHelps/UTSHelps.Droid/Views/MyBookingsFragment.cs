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
		private ProgressBar mBookingProgress;
		private BookedWorkshopFragment bookedFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			studentId = Arguments.GetString("studentId");
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyBookings, container, false);

            this.Activity.ActionBar.Title = "My Bookings";

            mBookingProgress = view.FindViewById<ProgressBar>(Resource.Id.mybooking_progress);
			mBookingProgress.Visibility = ViewStates.Visible;
			Refresh(studentId);

			bookingListView = view.FindViewById<ListView>(Resource.Id.lstCurrentBooking);

			adapter = new BookingsAdapter(this.Activity, mBookingWorkshops);
			bookingListView.Adapter = adapter;

			bookingListView.ItemClick += BookingListView_ItemClick;

            return view;
        }

		void BookingListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutString("studentId", studentId);
			args.PutInt("workshopId", mBookingWorkshops[e.Position].workshopId);

			bookedFragment = new BookedWorkshopFragment();
			bookedFragment.Arguments = args;
			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bookedFragment, "BookedFragment");
			trans.AddToBackStack(null);
			trans.Commit();
		}

		private async void Refresh(string studentId)
		{
			var response = await ServiceHelper.Booking.GetBookings(studentId);
			if (response.IsSuccess)
			{
				mBookingWorkshops = response.Results;
				adapter.SwapItems(mBookingWorkshops);
				mBookingProgress.Visibility = ViewStates.Gone;
			}
		}
    }
}