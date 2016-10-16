using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;


namespace UTSHelps.Droid
{
	public static class ColorHelper
	{
		private static List<Color> colors;

		public static Color GetWorkshopColor(Resources res, int id)
		{
			InitColors(res);

			Random rand = new Random(id);

			return colors[rand.Next(colors.Count - 1)];
		}

		static void InitColors(Resources res)
		{
			colors = new List<Color>();
			colors.Add(res.GetColor(Resource.Color.cat_1));
			colors.Add(res.GetColor(Resource.Color.cat_2));
			colors.Add(res.GetColor(Resource.Color.cat_3));
			colors.Add(res.GetColor(Resource.Color.cat_4));
			colors.Add(res.GetColor(Resource.Color.cat_5));
			colors.Add(res.GetColor(Resource.Color.cat_6));
			colors.Add(res.GetColor(Resource.Color.cat_7));
			colors.Add(res.GetColor(Resource.Color.cat_8));
			colors.Add(res.GetColor(Resource.Color.cat_9));
			colors.Add(res.GetColor(Resource.Color.cat_10));
			colors.Add(res.GetColor(Resource.Color.cat_11));
			colors.Add(res.GetColor(Resource.Color.cat_12));
			colors.Add(res.GetColor(Resource.Color.cat_13));
			colors.Add(res.GetColor(Resource.Color.cat_14));
			colors.Add(res.GetColor(Resource.Color.cat_15));
			colors.Add(res.GetColor(Resource.Color.cat_16));
			colors.Add(res.GetColor(Resource.Color.cat_17));
		}
	}
}
