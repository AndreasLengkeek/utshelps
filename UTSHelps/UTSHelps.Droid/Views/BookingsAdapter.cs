using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;

namespace UTSHelps.Droid
{
	public class BookingsAdapter : BaseAdapter<string>
	{
		private List<string> mBookingList;
		private Context context;

		public BookingsAdapter(Context context, List<string> bookingList)
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

		public override string this[int position]
		{
			get
			{
				return mBookingList[position];
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row== null)
			{
				row = LayoutInflater.From(context).Inflate(Resource.Layout.Adapter_Bookings, parent, false);
			}

			TextView txtWorkshop = row.FindViewById<TextView>(Resource.Id.txtWorkshop);
			txtWorkshop.Text = mBookingList[position];

			return row;
		}
	}
}
