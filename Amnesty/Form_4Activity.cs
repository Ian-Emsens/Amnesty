
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

using supAppCompat = Android.Support.V7.AppCompat;
using supToolbar = Android.Support.V7.Widget.Toolbar;
using supFAB = Android.Support.Design.Widget.FloatingActionButton;
using supDesign = Android.Support.Design;

namespace Amnesty
{
	[Activity (Label = "Form_4")]			
	public class Form_4 : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.Form_4);

			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var submit = FindViewById<Button> (Resource.Id.submit);
			var p1 = FindViewById<TextView> (Resource.Id.overview_01);
			var p2 = FindViewById<TextView> (Resource.Id.overview_02);
			var p3 = FindViewById<TextView> (Resource.Id.overview_03);
			var p4 = FindViewById<TextView> (Resource.Id.overview_04);
			var p5 = FindViewById<TextView> (Resource.Id.overview_05);
			var p6 = FindViewById<TextView> (Resource.Id.overview_06);
			var p7 = FindViewById<TextView> (Resource.Id.overview_07);
			var p8 = FindViewById<TextView> (Resource.Id.overview_08);
			var p9 = FindViewById<TextView> (Resource.Id.overview_09);
			var p10 = FindViewById<TextView> (Resource.Id.overview_10);

		// UI
			// Toolbar
			// Populate
			toolbar.Title = Resources.GetString (Resource.String.app_name);
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);

			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

		// Content
			// Populate
			p1.Text = p1.Text + Intent.GetStringExtra("strVolunteerName");
			p2.Text = p2.Text + Intent.GetStringExtra("strCharityCountry");
			p3.Text = p3.Text + Intent.GetStringExtra("strDonatorName") + " " + Intent.GetStringExtra("strDonatorLastname");
			p4.Text = p4.Text + Intent.GetStringExtra("strDonatorBirthdate");
			p5.Text = p5.Text + Intent.GetStringExtra("strDonatorStreet") + " " + Intent.GetStringExtra("strDonatorStreetNum");
			p6.Text = p6.Text + Intent.GetStringExtra("strDonatorProvince");
			p7.Text = p7.Text + Intent.GetStringExtra("strDonatorTel");
			p8.Text = p8.Text + Intent.GetStringExtra("strDonatorMail");
			p9.Text = p9.Text + Intent.GetStringExtra("strDonatorIban");
			p10.Text = p10.Text + Intent.GetStringExtra("strDonatorAmount");

		// Submit - Click
			submit.Click += delegate {
				var newIntent = new Intent (this, typeof(Landing));
				newIntent.PutExtra ("strVolunteerName", Intent.GetStringExtra("strVolunteerName"));
				StartActivity (newIntent);
			};
		}
	}
}

