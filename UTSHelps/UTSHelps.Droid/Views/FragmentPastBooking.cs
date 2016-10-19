
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
	public class FragmentPastBooking : Fragment
	{
		private BookedWorkshopFragment bookedFragment;
		private List<Booking> pastBooking;
		private String studentId;
		private ListView pastListView;
		private BookingsAdapter adapter;
		private ProgressBar pastProgress;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			pastBooking = new List<Booking>();
			studentId = Arguments.GetString("studentId");
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_PastBooking, container, false);
			Refresh(studentId, false);

			pastProgress = view.FindViewById<ProgressBar>(Resource.Id.past_progress);
			pastListView = view.FindViewById<ListView>(Resource.Id.pastBookingList);
			adapter = new BookingsAdapter(this.Activity, pastBooking);
			pastListView.Adapter = adapter;
			if (adapter.Count == 0)
			{
				pastProgress.Visibility = ViewStates.Visible;
			}
			else
			{
				pastProgress.Visibility = ViewStates.Gone;
			}

			pastListView.ItemClick += PastListView_ItemClick;

			return view;
		}

		void PastListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutString("studentId", studentId);
			bookedFragment = new BookedWorkshopFragment();
			bookedFragment.Arguments = args;

			var trans = FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bookedFragment, "BookedFragment");
			trans.AddToBackStack(null);
			trans.Commit();
		}

		async void Refresh(string studentId, bool v)
		{
			var response = await ServiceHelper.Booking.GetBookings(studentId, false);
			if (response.IsSuccess)
			{
				pastBooking = response.Results;
				adapter.SwapItems(pastBooking);
				pastProgress.Visibility = ViewStates.Gone;
			}
		}
	}
}
