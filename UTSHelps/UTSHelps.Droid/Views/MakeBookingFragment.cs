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
    public class MakeBookingFragment : Fragment
    {
		private List<WorkshopSet> workshopSets = new List<WorkshopSet>();
		private ListView workshopListView;
        private WorkshopAdapter workshopAdapter;
		private ProgressBar workshopSetsProgress;
		private SessionsFragment sFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
			Refresh();
			base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MakeBooking, container, false);

			//sets = new List<WorkshopSet>();
			workshopAdapter = new WorkshopAdapter(this.Activity, workshopSets);
            workshopListView = view.FindViewById<ListView>(Resource.Id.lstWorkshop);
            workshopListView.Adapter = workshopAdapter;

			workshopSetsProgress = view.FindViewById<ProgressBar>(Resource.Id.workshopsets_progress);
			if (workshopAdapter.Count == 0)
				workshopSetsProgress.Visibility = ViewStates.Visible;
			else
				workshopSetsProgress.Visibility = ViewStates.Gone;

			workshopListView.ItemClick += WorkshopListView_ItemClick;
            return view;
        }

		void WorkshopListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Bundle args = new Bundle();
			args.PutInt("workshopSetID", workshopSets[e.Position].Id);
			args.PutString("workshopSetName", workshopSets[e.Position].Name);

			sFragment = new SessionsFragment();
			sFragment.Arguments = args;
			FragmentTransaction transaction = this.Activity.FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.mainFragmentContainer, sFragment, "SessionFragment");
			transaction.AddToBackStack(null);
			transaction.Commit();
		}

        private async void Refresh()
        {
            var response = await ServiceHelper.Workshop.GetWorkshopSets();
            if (response.IsSuccess)
            {
				workshopSets = response.Results;
				workshopAdapter.SwapItems(workshopSets);
				workshopSetsProgress.Visibility = ViewStates.Gone;
            }
        }
    }
}