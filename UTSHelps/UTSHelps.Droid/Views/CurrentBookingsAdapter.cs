using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using UTSHelps.Shared.Models;
using Android.Views;

namespace UTSHelps.Droid
{
	public class CurrentBookingsAdapter : BaseAdapter<Booking>
	{
		private Context context;
		private List<Booking> mBookingList;
		public CurrentBookingsAdapter(Context context, List<Booking> mBookings)
		{
			this.context = context;
			mBookingList = mBookings;
		}

		public override int Count
		{
			get
			{
				return mBookingList.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override Booking this[int position]
		{
			get
			{
				return mBookingList[position];
			}
		}

		public void SwapItems(List<Booking> items)
		{
			this.mBookingList = items;
			NotifyDataSetChanged();
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row== null)
			{
				row = LayoutInflater.From(context).Inflate(Resource.Layout.Adapter_Bookings, parent, false);
			}

			TextView txtWorkshop = row.FindViewById<TextView>(Resource.Id.txtWorkshop);
			txtWorkshop.Text = mBookingList[position].topic;

			TextView txtWorkshopDay = row.FindViewById<TextView>(Resource.Id.day);
			txtWorkshopDay.Text = mBookingList[position].starting.ToString("ddd") + " " + mBookingList[position].starting.ToShortDateString();

			TextView txtWorkshopTime = row.FindViewById<TextView>(Resource.Id.time);
			txtWorkshopTime.Text = mBookingList[position].starting.ToString("hh:mm") + " - " + mBookingList[position].ending.ToString("hh:mm");

			TextView txtWorkshopLocation = row.FindViewById<TextView>(Resource.Id.locationName);
			txtWorkshopLocation.Text = mBookingList[position].campusID.ToString();

			TextView txtWorkshopDateStamp = row.FindViewById<TextView>(Resource.Id.date);
			txtWorkshopDateStamp.Text = mBookingList[position].starting.Subtract(DateTime.Now).ToString("dd");

			TextView txtWorkshopMonthStamp = row.FindViewById<TextView>(Resource.Id.month);
			txtWorkshopMonthStamp.Text = "Days";

			return row;
		}
	}
}
