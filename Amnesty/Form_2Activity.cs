using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

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
	[Activity (Label = "Form_2")]			
	public class Form_2 : Activity
	{
		// MICROSOFT EMAIL VALIDATOR
		bool invalid = false;

		public bool IsValidEmail (EditText obj)
		{
			invalid = false;
			String strIn = obj.EditableText.ToString();

			if (String.IsNullOrEmpty (strIn)) {
				obj.Error = Resources.GetString (Resource.String.error_empty);
				return false;
			}
			// Use IdnMapping class to convert Unicode domain names.
			try {
				strIn = Regex.Replace (strIn, @"(@)(.+)$", this.DomainMapper,
					RegexOptions.None, TimeSpan.FromMilliseconds (200));
			} catch (RegexMatchTimeoutException) {
				obj.Error = Resources.GetString(Resource.String.error_unknown);
				return false;
			}
			if (invalid) {
				obj.Error = Resources.GetString(Resource.String.error_format);
				return false;
			}
			// Return true if strIn is in valid e-mail format.
			try {
				return Regex.IsMatch (strIn,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds (250));
			} catch (RegexMatchTimeoutException) {
				obj.Error = Resources.GetString(Resource.String.error_unknown);
				return false;
			}
		}

		private string DomainMapper (Match match)
		{
			// IdnMapping class with default property values.
			IdnMapping idn = new IdnMapping ();
			string domainName = match.Groups [2].Value;
			try {
				domainName = idn.GetAscii (domainName);
			} catch (ArgumentException) {
				invalid = true;
			}
			return match.Groups [1].Value + domainName;
		}
		// END MICROSOFT EMAIL VALIDATOR

		// Checks if there are any problems with any fields and enables the button to continue if there aren't
		public Boolean Validate(EditText obj){
			if (String.IsNullOrWhiteSpace(obj.Text)) {
				obj.Error = Resources.GetString(Resource.String.error_empty);
				return false;
			} else {
				return true;
			}
		}

		public void Activate(Button x, EditText a, EditText b, EditText c){
			if (Validate (a) &&
			    IsValidEmail (b) &&
			    Validate (c)) {
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

		public void populateIban(EditText a){
			if(String.IsNullOrEmpty(a.EditableText.ToString()))
				a.Text = a.Text + Resources.GetString(Resource.String.ui_iban);
		}
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Form_2);

			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var next = FindViewById<Button> (Resource.Id.next);				// Next Button
			var telephone = FindViewById<EditText> (Resource.Id.telephone);	// Telephone Field
			var mail = FindViewById<EditText> (Resource.Id.mail);				// Mail Field
			var iban = FindViewById<EditText> (Resource.Id.iban);				// Iban Field

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
				var newIntent = new Intent (this, typeof(Form_3));

				newIntent.PutExtra ("strCharityCountry", Intent.GetStringExtra("strCharityCountry")); // IMPROV: Change to ID
				newIntent.PutExtra ("strVolunteerName", Intent.GetStringExtra("strVolunteerName")); // IMPROV: Change to ID
				newIntent.PutExtra ("strDonatorName", Intent.GetStringExtra("strDonatorName"));
				newIntent.PutExtra ("strDonatorLastname", Intent.GetStringExtra("strDonatorLastname"));
				newIntent.PutExtra ("strDonatorBirthdate", Intent.GetStringExtra("strDonatorBirthdate"));
				newIntent.PutExtra ("strDonatorStreet", Intent.GetStringExtra("strDonatorStreet"));
				newIntent.PutExtra ("strDonatorStreetNum", Intent.GetStringExtra("strDonatorStreetNum"));
				newIntent.PutExtra ("strDonatorProvince", Intent.GetStringExtra("strDonatorProvince"));

				newIntent.PutExtra ("strDonatorTel", telephone.EditableText.ToString());
				newIntent.PutExtra ("strDonatorMail", mail.EditableText.ToString());
				newIntent.PutExtra ("strDonatorIban", iban.EditableText.ToString());

				StartActivity (newIntent);	
			};

			// Telephone Field Validation
			telephone.TextChanged += delegate {
				Activate (next, telephone, mail, iban);
			};

			telephone.FocusChange += delegate {
				Activate (next, telephone, mail, iban);
			};

			// Mail Field Validation
			mail.TextChanged += delegate {
				Activate (next, telephone, mail, iban);
			};

			mail.FocusChange += delegate {
				Activate (next, telephone, mail, iban);
			};

			// Iban Field Validation
			iban.TextChanged += delegate {
				Activate (next, telephone, mail, iban);
			};

			iban.FocusChange += delegate {
				Activate (next, telephone, mail, iban);
				populateIban(iban);
			};
		}
	}
}

