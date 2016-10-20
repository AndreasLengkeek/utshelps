using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using UTSHelps.Shared.Models;
using UTSHelps.Droid.Helpers;

namespace UTSHelps.Droid
{
	public class BookingsAdapter : BaseAdapter<Booking>
	{
		private List<Booking> mBookingList;
		private Context context;

		public BookingsAdapter(Context context, List<Booking> bookingList)
		{
			mBookingList = bookingList;
			this.context = context;
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

			TextView txtWorkshopDateStamp = row.FindViewById<TextView>(Resource.Id.date);
			txtWorkshopDateStamp.Text = mBookingList[position].starting.ToString("dd");

			TextView txtWorkshopMonthStamp = row.FindViewById<TextView>(Resource.Id.month);
			txtWorkshopMonthStamp.Text = mBookingList[position].starting.ToString("MMM");


            var attended = mBookingList[position].attended.HasValue && mBookingList[position].attended.Value == 1 
                                            ? "Attended" : "Not Attended";

            FontAwesome attendedIcon = row.FindViewById<FontAwesome>(Resource.Id.txtAttendedIcon);
            if (attended == "Not Attended")
            {
                attendedIcon.Text = context.Resources.GetString(Resource.String.fa_times);
            }
            else
            {
                attendedIcon.Text = context.Resources.GetString(Resource.String.fa_check);
            }

            TextView txtAttended = row.FindViewById<TextView>(Resource.Id.txtAttended);
            txtAttended.Text = attended;

            return row;
		}
	}
}
