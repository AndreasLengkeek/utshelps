using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Icu.Util;

namespace UTSHelps.Droid.Helpers
{
    public static class DialogHelper
    {
        public static void ShowDialog(Context context, string title, string message)
        {
            var builder = new AlertDialog.Builder(context);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetPositiveButton("Ok", (senderAlert, args) => { });
            builder.Create().Show();
        }

        public static ProgressDialog CreateProgressDialog(string message, Activity activity)
        {
            var progDialog = new ProgressDialog(activity);
            progDialog.SetMessage(message);
            progDialog.SetCancelable(false);
            progDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            return progDialog;
        }

        public static DatePickerDialog ShowDatePickerDialog(Context ctx, TextView text)
        {
            try
            {
                var cal = Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
                var listener = new OnDateSetListener(text);
                var datePicker = new DatePickerDialog(ctx, listener,
                    cal.Get(CalendarField.Year),
                    cal.Get(CalendarField.Month),
                    cal.Get(CalendarField.DayOfMonth));
                datePicker.SetCancelable(false);
                datePicker.Show();
            }
            catch (Exception ex)
            {
                ShowDialog(ctx, "An error occured while showing Date Picker\n\n Error Details:\n" + ex, "Exception");
            }
            return null;
        }

        private class OnDateSetListener : Java.Lang.Object, DatePickerDialog.IOnDateSetListener
        {
            private TextView textView;

            public OnDateSetListener(TextView text)
            {
                this.textView = text;
            }

            // when dialog box is closed, below method will be called.
            public void OnDateSet(DatePicker view, int selectedYear,
                int selectedMonth, int selectedDay)
            {
                var date = new DateTime(selectedYear, selectedMonth + 1, selectedDay);
                var formattedDateTime = date.ToString("ddd, dd MMM yyyy");
                textView.Text = formattedDateTime;
            }
        }
    }
}