using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;

namespace UTSHelps.Droid
{
	public class SessionAdapter : BaseAdapter<string>
	{
		private List<string> sessions;
		private Context context;

		public SessionAdapter(Context context, List<string> sessionsList)
		{
			this.context = context;
			sessions = sessionsList;
		}

		public override int Count
		{
			get
			{
				return sessions.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position]
		{
			get
			{
				return sessions[position];
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(context).Inflate(Resource.Layout.Adapter_Session, parent, false);
			}

			TextView sessionName = row.FindViewById<TextView>(Resource.Id.sessionName);
			sessionName.Text = sessions[position];

			return row;
		}
	}
}
