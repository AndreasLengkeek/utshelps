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
		private string studentId;
		private ProgressBar mBookingProgress;
		private FragmentCurrentBooking currentBooking;
		private FragmentPastBooking pastBooking;
		private LinearLayout currentBookingLayout;
		private LinearLayout pastBookingLayout;
		private View currentSignifier;
		private View pastSignifier;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			studentId = Arguments.GetString("studentId");
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyBookings, container, false);

            this.Activity.ActionBar.Title = "My Bookings";
			Bundle bundle = new Bundle();
			bundle.PutString("studentId", studentId);

			currentBooking = new FragmentCurrentBooking();
			currentBooking.Arguments = bundle;
			pastBooking = new FragmentPastBooking();
			pastBooking.Arguments = bundle;

			currentBookingLayout = view.FindViewById<LinearLayout>(Resource.Id.currentBookingTab);
			pastBookingLayout = view.FindViewById<LinearLayout>(Resource.Id.pastBookingTab);
			currentSignifier = view.FindViewById<View>(Resource.Id.currentTabSignifier);
			pastSignifier = view.FindViewById<View>(Resource.Id.pastTabSignifier);

			currentBookingLayout.Click += CurrentBookingLayout_Click;
			pastBookingLayout.Click += PastBookingLayout_Click;

			var trans = FragmentManager.BeginTransaction();
			trans.Add(Resource.Id.myBookingFragment, currentBooking, "Current Booking");
			trans.Commit();

            return view;
        }

		void CurrentBookingLayout_Click(object sender, EventArgs e)
		{
			currentSignifier.Visibility = ViewStates.Visible;
			pastSignifier.Visibility = ViewStates.Gone;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.myBookingFragment, currentBooking, "Current Booking");
			trans.Commit();
		}

		void PastBookingLayout_Click(object sender, EventArgs e)
		{
			currentSignifier.Visibility = ViewStates.Gone;
			pastSignifier.Visibility = ViewStates.Visible;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.myBookingFragment, pastBooking, "Past Booking");
			trans.Commit();
		}

		void BookingListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutString("studentId", studentId);
			BookedWorkshopFragment bookedFragment = new BookedWorkshopFragment();
			bookedFragment.Arguments = args;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bookedFragment, "BookedFragment");
			trans.AddToBackStack(null);
			trans.Commit();
		}
    }
}