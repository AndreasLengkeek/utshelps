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

namespace UTSHelps.Droid
{
    [Activity(Label = "Main")]
    public abstract class MainActivity : Activity
    {
        //public User CurrentUser;
        protected abstract int LayoutResource { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set child layout
            SetContentView(LayoutResource);

            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //if (toolbar != null)
            //{
            //    SetActionBar(toolbar);
            //    ActionBar.Title = "My Toolbar"; 
            //}

            //var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
            //if (editToolbar != null)
            //{
            //    editToolbar.Title = "Editing";
            //    editToolbar.InflateMenu(Resource.Menu.edit_menus);
            //    editToolbar.MenuItemClick += (sender, e) => {
            //        Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            //    }; 
            //}
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.top_menus, menu);
        //    return base.OnCreateOptionsMenu(menu);
        //}
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
        //        ToastLength.Short).Show();
        //    return base.OnOptionsItemSelected(item);
        //}
    }
}