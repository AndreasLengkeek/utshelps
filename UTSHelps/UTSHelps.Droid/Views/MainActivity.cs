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
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
    [Activity(Label = "Main")]
    public abstract class MainActivity : Activity
    {
        public Student CurrentUser;
        protected abstract int LayoutResource { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set child layout
            SetContentView(LayoutResource);
        }
    }
}