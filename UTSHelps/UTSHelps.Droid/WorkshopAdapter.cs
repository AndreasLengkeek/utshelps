using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
	public class WorkshopAdapter : BaseAdapter<WorkshopSet>
	{
		private List<WorkshopSet> workshops;
		private Context context;

		public WorkshopAdapter(Context context, List<WorkshopSet> list)
		{
			this.context = context;
			workshops = list;
		}

		public override int Count
		{
			get
			{
				return workshops.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override WorkshopSet this[int position]
		{
			get
			{
				return workshops[position];
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(this.context).Inflate(Resource.Layout.Adapter_Workshops, parent, false);
			}

			TextView mWorkshop = row.FindViewById<TextView>(Resource.Id.workshopSets_text);
			mWorkshop.Text = workshops[position].Name;

			return row;
		}
	}
}
