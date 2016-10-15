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
using System.Globalization;

namespace UTSHelps.Droid
{
	public class SessionsFragment : Fragment
	{
		private List<Workshop> sessions;
		private List<Workshop> sessionList;
        private ListView sessionListView;
		private SessionAdapter sessionAdapter;
		private ProgressBar sessionsProgressBar;
		private BookingWorkshopFragment bWorkshopFragment;

        private int workshopSetID;
        private string workshopSetName;

        private string studentId;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            sessions = new List<Workshop>();
            sessionList = new List<Workshop>();

            Bundle args = Arguments;
			workshopSetID = args.GetInt("workshopSetID");
			workshopSetName = args.GetString("workshopSetName");
			studentId = args.GetString("studentId");

			Refresh(workshopSetID);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Sessions, container, false);

            SetView(view);

            return view;
        }

        private void SetView(View view)
        {
            this.Activity.ActionBar.Title = "Choose a session";
            var searchBtn = view.FindViewById<Button>(Resource.Id.btnWorkshopSearch);
            searchBtn.Click += Search;

            TextView workshopSetsName = view.FindViewById<TextView>(Resource.Id.session_workshopName);
            workshopSetsName.Text = workshopSetName;

            sessionAdapter = new SessionAdapter(this.Activity, sessionList);
            sessionListView = view.FindViewById<ListView>(Resource.Id.lstSessions);
            sessionListView.Adapter = sessionAdapter;
            Toast.MakeText(this.Activity, "The WorkshopSets Id is " + workshopSetID, ToastLength.Short).Show();

            sessionsProgressBar = view.FindViewById<ProgressBar>(Resource.Id.session_progress);
            sessionsProgressBar.Visibility = ViewStates.Visible;

            sessionListView.ItemClick += SessionListView_ItemClick;
        }

        void SessionListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutInt("workshopId", sessionList[e.Position].WorkshopId);
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
                sessions = response.Results;
                sessionAdapter.SwapItems(sessions);
                sessionsProgressBar.Visibility = ViewStates.Gone;
            }
        }

        private void Search(object sender, EventArgs e)
        {
            var topic = this.View.FindViewById<TextView>(Resource.Id.searchTopic).Text;
            var date = this.View.FindViewById<TextView>(Resource.Id.searchDate).Text;
            var campus = this.View.FindViewById<TextView>(Resource.Id.searchCampus).Text;

            IEnumerable<Workshop> query = sessions.ToList();
            if (!String.IsNullOrEmpty(topic))
            {
                query = query.Where(w => w.topic.ToLower().Contains(topic.ToLower()));
            }
            if (!String.IsNullOrEmpty(campus))
            {
                query = query.Where(w => w.campus.ToLower().Contains(campus.ToLower()));
            }
            if (!String.IsNullOrEmpty(date))
            {
                var start = ParseDate(date);
                query = query.Where(w => w.StartDate > start);
            }

            sessionList = query.ToList();
            sessionAdapter.SwapItems(sessionList);
        }

        private DateTime ParseDate(string date)
        {
            return DateTime.ParseExact(date, "ddd, dd MMM yyyy", CultureInfo.InvariantCulture);
        }
    }
}
