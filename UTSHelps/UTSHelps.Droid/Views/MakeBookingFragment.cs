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
		private List<string> workshops;
		private ListView workshopListView;
		private SessionsFragment sFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MakeBooking, container, false);

            var t = Task.Run(async () => {
                var response = await ServiceHelper.Workshop.GetWorkshopSets();
                if (response.IsSuccess)
                {
                    List<WorkshopSet> sets = response.Results;
                    var workshopAdap = new WorkshopAdapter(this.Activity, sets);

                    workshopListView = view.FindViewById<ListView>(Resource.Id.lstWorkshop);
                    workshopListView.Adapter = workshopAdap;
                }
            });
            t.Wait();

            return view;
        }
	}
}