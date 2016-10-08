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
    }
}