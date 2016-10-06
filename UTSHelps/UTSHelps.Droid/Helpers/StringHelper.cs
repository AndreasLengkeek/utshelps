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
    public static class StringHelper
    {
        public static int ToInt(this string str)
        {
            int value;
            Int32.TryParse(str, out value);

            return value;
        }
    }
}