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
using UTSHelps.Shared.Models;
using System.Globalization;
using UTSHelps.Droid.ViewModels;

namespace UTSHelps.Droid
{
    public class RegistrationStudentInfoFragment : Fragment
    {
        private TextView prefName;
        private TextView altContact;
        private TextView date;
        private TextView month;
        private TextView year;
        private Spinner country;
        private Spinner language;
        private RadioGroup status;
        private RadioGroup gender;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_RegistrationStudentInfo, container, false);

            InitComponents(view);

            return view;
        }

        private void InitComponents(View view)
        {
            prefName = view.FindViewById<TextView>(Resource.Id.rego2PreferredFIrstName);
            altContact = view.FindViewById<TextView>(Resource.Id.Rego2BestContactNo);
            date = view.FindViewById<TextView>(Resource.Id.regDobDate);
            month = view.FindViewById<TextView>(Resource.Id.regDobMonth);
            year = view.FindViewById<TextView>(Resource.Id.regDobYear);
            country = view.FindViewById<Spinner>(Resource.Id.regCountrySpinner);
            language = view.FindViewById<Spinner>(Resource.Id.regLanguageSpinner);
            status = view.FindViewById<RadioGroup>(Resource.Id.regStatusRadioGroup);
            gender = view.FindViewById<RadioGroup>(Resource.Id.regGenderRadioGroup);

            country.Adapter = GetCountries();
            language.Adapter = GetLanguages();
        }

        public RegistrationStudentInfoViewModel GetData()
        {
            var birthDate = GetBirthDate(date, month, year);
            var genderSelected = this.View.FindViewById<RadioButton>(gender.CheckedRadioButtonId);
            var statusSelected = this.View.FindViewById<RadioButton>(status.CheckedRadioButtonId);

            return new RegistrationStudentInfoViewModel {
                PrefName = prefName.Text,
                AltContact = altContact.Text,
                BirthDate = birthDate,
                Country = country.SelectedItem.ToString(),
                Language = language.SelectedItem.ToString(),
                Status = status.IndexOfChild(statusSelected),
                Gender = gender.IndexOfChild(genderSelected)
            };
        }

        private ArrayAdapter GetCountries(string overrideValue = "Australia")
        {

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            LinkedList<string> countries = new LinkedList<string>();
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    RegionInfo regionInfo = new RegionInfo(culture.LCID);
                    if (!(countries.Contains(regionInfo.EnglishName)))
                        countries.AddLast(regionInfo.EnglishName);
                }
                catch { }
            }
            return SortAndAssign(countries, overrideValue);
        }

        private ArrayAdapter GetLanguages(string overrideValue = "English")
        {

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            LinkedList<string> languages = new LinkedList<string>();
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    if (culture.IsNeutralCulture)
                        languages.AddLast(culture.DisplayName);
                }
                catch { }
            }
            return SortAndAssign(languages, overrideValue);
        }

        private ArrayAdapter SortAndAssign(LinkedList<string> list, string DefaultValue)
        {
            List<string> sortedList = list.ToList();
            sortedList.Sort();
            sortedList.Insert(0, DefaultValue);
            ArrayAdapter arr = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line,
                sortedList.ToArray<string>());
            arr.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            return arr;
        }

        private static DateTime GetBirthDate(TextView date, TextView month, TextView year)
        {
            DateTime dt;
            try
            {
                dt = DateTime.ParseExact(year.Text + month.Text + date.Text, "yyyyMMdd", CultureInfo.InvariantCulture);
            } catch (Exception ex)
            {
                Console.Write(ex.Message);
                dt = DateTime.Now;
            }
            return dt;
        }
    }
}