
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
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
	public class FragmentCurrentBooking : Fragment
	{
		private List<Booking> currentBooking;
		private BookingsAdapter adapter;
		private ListView currentListView;
		private BookedWorkshopFragment bookedFragment;
		private ProgressBar currentProgress;
		private String studentId;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			currentBooking = new List<Booking>();
			studentId = Arguments.GetString("studentId");
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_CurrentBooking, container, false);
			Refresh(studentId, true);
			currentProgress = view.FindViewById<ProgressBar>(Resource.Id.current_progress);
			currentListView = view.FindViewById<ListView>(Resource.Id.currentBookingList);
			adapter = new BookingsAdapter(this.Activity, currentBooking);
			currentListView.Adapter = adapter;
			if (adapter.Count == 0)
			{
				currentProgress.Visibility = ViewStates.Visible;
			}
			else
			{
				currentProgress.Visibility = ViewStates.Gone;
			}

			currentListView.ItemClick += CurrentListView_ItemClick;
			return view;
		}

		void CurrentListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutString("studentId", studentId);
			args.PutInt("workshopId", currentBooking[e.Position].workshopId);
			bookedFragment = new BookedWorkshopFragment();
			bookedFragment.Arguments = args;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bookedFragment, "BookedFragment");
			trans.AddToBackStack(null);
			trans.Commit();
		}


		async void Refresh(string studentId, bool v)
		{
			var response = await ServiceHelper.Booking.GetBookings(studentId, v);
			if (response.IsSuccess)
			{
				currentBooking = response.Results;
				adapter.SwapItems(currentBooking);
				currentProgress.Visibility = ViewStates.Gone;
			}
		}
}
}
