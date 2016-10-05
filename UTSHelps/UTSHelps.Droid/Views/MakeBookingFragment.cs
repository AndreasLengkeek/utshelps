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
		private List<WorkshopSet> sets = new List<WorkshopSet>();
		private ListView workshopListView;
        private WorkshopAdapter workshopAdapter;
		private ProgressBar workshopSetsProgress;

        public override async void OnCreate(Bundle savedInstanceState)
        {
			Refresh();
			base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MakeBooking, container, false);

			//sets = new List<WorkshopSet>();
            workshopAdapter = new WorkshopAdapter(this.Activity, sets);
            workshopListView = view.FindViewById<ListView>(Resource.Id.lstWorkshop);
            workshopListView.Adapter = workshopAdapter;

			workshopSetsProgress = view.FindViewById<ProgressBar>(Resource.Id.workshopsets_progress);
			if (workshopAdapter.Count == 0)
			{
				workshopSetsProgress.Visibility = ViewStates.Visible;
			}
			else
			{
				workshopSetsProgress.Visibility = ViewStates.Gone;
			}

            //UpdateView();

            return view;
        }

    //    private void UpdateView()
    //    {
    //        if (workshopAdapter != null)
    //        {
				//Activity.RunOnUiThread(new Action(() =>
				//{
    //            	workshopAdapter.NotifyDataSetChanged();
				//}));
    //        }
    //    }

        private async void Refresh()
        {
            var response = await ServiceHelper.Workshop.GetWorkshopSets();
            if (response.IsSuccess)
            {
                sets = response.Results;
				workshopAdapter.SwapItems(sets);
				workshopSetsProgress.Visibility = ViewStates.Gone;
            }
        }
    }
}