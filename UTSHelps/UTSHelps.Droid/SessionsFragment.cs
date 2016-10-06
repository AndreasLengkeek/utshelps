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

namespace UTSHelps.Droid
{
	public class SessionsFragment : Fragment
	{
		private List<string> sets = new List<string>();
		private ListView sessionListView;
		private SessionAdapter sessionAdapter;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Fragment_Sessions, container, false);
			Bundle args = Arguments;
			int workshopId = args.GetInt("workshopID");
			sets.Add("My Ass is burning");
			sets.Add("So does my Butt");
			sessionAdapter = new SessionAdapter(this.Activity, sets);
			sessionListView = view.FindViewById<ListView>(Resource.Id.lstSessions);
			sessionListView.Adapter = sessionAdapter;
			Toast.MakeText(this.Activity, "The Workshop Id is " + workshopId, ToastLength.Short).Show();

			return view;
		}
	}
}
