using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using UTSHelps.Droid.ViewModels;

namespace UTSHelps.Droid
{
    public class RegistrationCourseFragment : Fragment
    {
        private RadioGroup degree;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_RegistrationCourseInfo, container, false);

            InitComponents(view);

            return view;
        }

        private void InitComponents(View view)
        {
            degree = view.FindViewById<RadioGroup>(Resource.Id.regDegreeRadioGroup);
        }

        public RegistrationCourseViewModel GetData()
        {
            var selectedDegree = View.FindViewById<RadioButton>(degree.CheckedRadioButtonId);

            return new RegistrationCourseViewModel {
                Degree = degree.IndexOfChild(selectedDegree)
            };
        }
    }
}