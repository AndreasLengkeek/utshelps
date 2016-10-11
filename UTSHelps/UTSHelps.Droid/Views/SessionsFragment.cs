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
	public class SessionsFragment : Fragment
	{
		private List<Workshop> sessionSets = new List<Workshop>();
		private ListView sessionListView;
		private SessionAdapter sessionAdapter;
		private ProgressBar sessionsProgressBar;
		private int workshopSetID;
        private String workshopSetName;
		private BookingWorkshopFragment bWorkshopFragment;
		private String studentId;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Bundle args = Arguments;
			workshopSetID = args.GetInt("workshopSetID");
			workshopSetName = args.GetString("workshopSetName");
			studentId = args.GetString("studentId");

			Refresh(workshopSetID);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_Sessions, container, false);

			TextView workshopSetsName = view.FindViewById<TextView>(Resource.Id.session_workshopName);
			workshopSetsName.Text = workshopSetName;

			sessionAdapter = new SessionAdapter(this.Activity, sessionSets);
			sessionListView = view.FindViewById<ListView>(Resource.Id.lstSessions);
			sessionListView.Adapter = sessionAdapter;
			Toast.MakeText(this.Activity, "The WorkshopSets Id is " + workshopSetID, ToastLength.Short).Show();

            sessionsProgressBar = view.FindViewById<ProgressBar>(Resource.Id.session_progress);
            if (sessionAdapter.Count == 0)
            {
                sessionsProgressBar.Visibility = ViewStates.Visible;
            }
            else
            {
                sessionsProgressBar.Visibility = ViewStates.Gone;
            }

			sessionListView.ItemClick += SessionListView_ItemClick;

            return view;
		}

		void SessionListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutInt("workshopId", sessionSets[e.Position].WorkshopId);
			args.PutString("studentId", studentId);

			bWorkshopFragment = new BookingWorkshopFragment();
			bWorkshopFragment.Arguments = args;
			FragmentTransaction trans = this.Activity.FragmentManager.BeginTransaction();
			trans.Replace(Resource.Id.mainFragmentContainer, bWorkshopFragment, "BookingWorkshopFragment");
			trans.AddToBackStack(null);
			trans.Commit();
		}

		private async void Refresh(int workshopSetId)
		{
			var response = await ServiceHelper.Workshop.GetWorkshops(workshopSetId);
			if (response.IsSuccess)
			{
                sessionSets = response.Results;
                sessionAdapter.SwapItems(sessionSets);
                sessionsProgressBar.Visibility = ViewStates.Gone;
            }
		}
	}
}
