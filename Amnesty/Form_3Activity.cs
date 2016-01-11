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
	[Activity (Label = "Form_3")]			
	public class Form_3 : Activity
	{

		// Checks if there are any problems with any fields and enables the button to continue if there aren't
		public Boolean Validate(EditText obj){
			if (String.IsNullOrWhiteSpace(obj.Text)) {
				obj.Error = Resources.GetString(Resource.String.error_empty);
				return false;
			} else {
				return true;
			}
		}

		public void Activate(Button x, EditText a){
			if (Validate (a)) {
				var next = FindViewById<Button> (Resource.Id.next);
				next.Enabled = true;
				next.SetBackgroundColor (Android.Graphics.Color.Rgb (255, 237, 0));
				next.SetTextColor (Android.Graphics.Color.Black);
			} else if(x.Enabled) {
				x.Enabled = false;
				x.SetBackgroundColor (Android.Graphics.Color.Rgb (225, 225, 225));
				x.SetTextColor (Android.Graphics.Color.Rgb (175, 175, 175));
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Form_3);

			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var next = FindViewById<Button> (Resource.Id.next);				// Next Button
			var donation = FindViewById<EditText> (Resource.Id.donation);	// Amount Field

			// UI
			// Toolbar
			// Populate
			toolbar.Title = Resources.GetString (Resource.String.app_name);
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);

			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

			// Button
			// Disable button by default
			next.Enabled = true; // DEBUG - switch to false
			next.SetBackgroundColor (Android.Graphics.Color.Rgb (225, 225, 225));
			next.SetTextColor (Android.Graphics.Color.Rgb (175, 175, 175));

			// Next Button
			next.Click += delegate {
				var newIntent = new Intent (this, typeof(Form_4));

				newIntent.PutExtra ("strCharityCountry", Intent.GetStringExtra("strCharityCountry")); // IMPROV: Change to ID
				newIntent.PutExtra ("strVolunteerName", Intent.GetStringExtra("strVolunteerName")); // IMPROV: Change to ID
				newIntent.PutExtra ("strDonatorName", Intent.GetStringExtra("strDonatorName"));
				newIntent.PutExtra ("strDonatorLastname", Intent.GetStringExtra("strDonatorLastname"));
				newIntent.PutExtra ("strDonatorBirthdate", Intent.GetStringExtra("strDonatorBirthdate"));
				newIntent.PutExtra ("strDonatorStreet", Intent.GetStringExtra("strDonatorStreet"));
				newIntent.PutExtra ("strDonatorStreetNum", Intent.GetStringExtra("intDonatorStreetNum"));
				newIntent.PutExtra ("strDonatorProvince", Intent.GetStringExtra("strDonatorProvince"));
				newIntent.PutExtra ("strDonatorTel", Intent.GetStringExtra("strDonatorTel"));
				newIntent.PutExtra ("strDonatorMail", Intent.GetStringExtra("strDonatorMail"));
				newIntent.PutExtra ("strDonatorIban", Intent.GetStringExtra("strDonatorIban"));

				newIntent.PutExtra ("strDonatorAmount", donation.EditableText.ToString());

				StartActivity (newIntent);	
			};

			// Birthdate Field Validation
			donation.FocusChange += delegate {
				Activate(next, donation);
			};
			donation.TextChanged += delegate {
				Activate(next, donation);
			};
		}
	}
}

