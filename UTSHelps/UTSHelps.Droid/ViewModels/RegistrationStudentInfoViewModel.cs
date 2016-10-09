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

namespace UTSHelps.Droid.ViewModels
{
    public class RegistrationStudentInfoViewModel
    {
        public DateTime? BirthDate { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string AltContact { get; set; }
        public string PrefName { get; set; }
    }
}