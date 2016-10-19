using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UTSHelps.Droid.Helpers;
using Android.Graphics;
using UTSHelps.Shared.Models;
using System.Globalization;

namespace UTSHelps.Droid
{
	[Activity(Label = "RegisterActivity", ScreenOrientation= ScreenOrientation.Portrait)]
    public class RegisterActivity : MainActivity
    {
        private string studentId;

        private TextView prefName;
        private TextView altContact;
        private TextView date;
        private TextView month;
        private TextView year;
        private Spinner country;
        private Spinner language;
        private RadioGroup status;
        private RadioGroup gender;
        private RadioGroup degree;

        protected override int LayoutResource
        {
            get {
                return Resource.Layout.Activity_Registration;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitComponents();

            studentId = Intent.GetStringExtra("studentId");
        }

        private void InitComponents()
        {
            prefName = FindViewById<TextView>(Resource.Id.rego2PreferredFIrstName);
            altContact = FindViewById<TextView>(Resource.Id.Rego2BestContactNo);
            date = FindViewById<TextView>(Resource.Id.regDobDate);
            month = FindViewById<TextView>(Resource.Id.regDobMonth);
            year = FindViewById<TextView>(Resource.Id.regDobYear);
            country = FindViewById<Spinner>(Resource.Id.regCountrySpinner);
            language = FindViewById<Spinner>(Resource.Id.regLanguageSpinner);
            status = FindViewById<RadioGroup>(Resource.Id.regStatusRadioGroup);
            gender = FindViewById<RadioGroup>(Resource.Id.regGenderRadioGroup);
            degree = FindViewById<RadioGroup>(Resource.Id.regDegreeRadioGroup);

            country.Adapter = GetCountries();
            language.Adapter = GetLanguages();
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
            ArrayAdapter arr = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line,
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
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                dt = DateTime.Now;
            }
            return dt;
        }

        [Java.Interop.Export()]
        public void EditDate(View view)
        {
            DialogHelper.ShowDatePickerDialog(this, (TextView)view);
        }

        [Java.Interop.Export()]
        public async void Register(View view)
        {
            var newStudent = ParseView();

            if (newStudent == null)
            {
                DialogHelper.ShowDialog(this, "An error occured", "You need to fill in all the fields");
                return;
            }

            var progressDialog = DialogHelper.CreateProgressDialog("Registering...", this);
            progressDialog.Show();
            var response = await ServiceHelper.Student.Register(newStudent);
            progressDialog.Hide();

            if (response.IsSuccess)
            {
                var intent = new Intent(this, typeof(RegistrationConfirmationActivity));
                intent.PutExtra("studentId", newStudent.StudentId);
                this.StartActivity(intent);
            }
            else
            {
                DialogHelper.ShowDialog(this, "An error occured", response.DisplayMessage);
            }
        }

        private RegisterRequest ParseView()
        {
            var birthDate = GetBirthDate(date, month, year);
            var statusSelected = FindViewById<RadioButton>(status.CheckedRadioButtonId);
            var degreeSelected = FindViewById<RadioButton>(degree.CheckedRadioButtonId);
            var genderSelected = FindViewById<RadioButton>(gender.CheckedRadioButtonId);

            if (BlankData())
            {
                return null;
            }

            var newStudent = new RegisterRequest {
                StudentId = studentId,
                PreferredName = prefName.Text,
                AltContact = altContact.Text,
                DateOfBirth = birthDate,
                CountryOrigin = country.SelectedItem.ToString(),
                FirstLanguage = language.SelectedItem.ToString(),
                Status = status.IndexOfChild(statusSelected),
                Degree = degree.IndexOfChild(degreeSelected) + 1,
                Gender = gender.IndexOfChild(genderSelected)
            };

            return newStudent;
        }

        private bool BlankData()
        {
            return String.IsNullOrEmpty(prefName.Text) || String.IsNullOrEmpty(altContact.Text)
                || String.IsNullOrEmpty(date.Text) || String.IsNullOrEmpty(month.Text) || String.IsNullOrEmpty(year.Text);
        }
    }
}