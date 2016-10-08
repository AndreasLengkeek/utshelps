﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
	public class SessionAdapter : BaseAdapter<Workshop>
	{
		private List<Workshop> sessions;
		private Context context;

		public SessionAdapter(Context context, List<Workshop> sessionsList)
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

		public override Workshop this[int position]
		{
			get
			{
				return sessions[position];
			}
		}

		public void SwapItems(List<Workshop> items)
		{
			this.sessions = items;
			NotifyDataSetChanged();
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(context).Inflate(Resource.Layout.Adapter_Session, parent, false);
			}

			TextView sessionName = row.FindViewById<TextView>(Resource.Id.sessionName);
			sessionName.Text = sessions[position].topic;

			TextView sessionLocation = row.FindViewById<TextView>(Resource.Id.sessionLocation);
			sessionLocation.Text = sessions[position].campus;

			TextView sessionAvailability = row.FindViewById<TextView>(Resource.Id.noOfSession);
			sessionAvailability.Text = sessions[position].NumOfWeeks.ToString();

			TextView sessionStartDate = row.FindViewById<TextView>(Resource.Id.sessionStartDate);
			sessionStartDate.Text = sessions[position].StartDate.ToShortDateString();

			TextView sessionEndDate = row.FindViewById<TextView>(Resource.Id.sessionEndDate);
			sessionEndDate.Text = sessions[position].EndDate.ToShortDateString();

			TextView sessionTime = row.FindViewById<TextView>(Resource.Id.sessionTime);
			var time = sessions[position].StartDate.ToString("hhtt") + " - " + sessions[position].EndDate.ToString("hhtt");
			sessionTime.Text = time;

			TextView sessionPlace = row.FindViewById<TextView>(Resource.Id.sessionPlace);
			var placesLeft = sessions[position].maximum - sessions[position].BookingCount;
			sessionPlace.Text = placesLeft.ToString();

			return row;
		}
	}
}
