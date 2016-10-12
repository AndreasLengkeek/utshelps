
using System;
using System.Collections.Generic;
using System.Globalization;
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
	[Activity(Label = "StudentDetailsActivity")]
	public class StudentDetailsActivity : MainActivity
	{
		private Spinner country;
		private Spinner language;
		protected override int LayoutResource
		{
			get
			{
				return Resource.Layout.Fragment_MyDetails;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			country = FindViewById<Spinner>(Resource.Id.regCountrySpinner);
			language = FindViewById<Spinner>(Resource.Id.regLanguageSpinner);

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
	}
}
