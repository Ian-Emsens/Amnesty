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
	[Activity (Label = "Form_1")]			
	public class Form_1 : Activity
	{
		const int DATE_DIALOG_ID = 0;
		DateTime currentDate = DateTime.Today.AddYears(-18);
		DateTime initDate;

		public Boolean Validate(EditText obj){
			if (String.IsNullOrWhiteSpace(obj.Text.ToString())) {
				obj.Error = Resources.GetString(Resource.String.error_empty);
				return false;
			} else {
				return true;
			}
		}

		public void Activate(Button x, EditText a, EditText b, EditText c, EditText d, EditText e, AutoCompleteTextView f){
			if (Validate(a) &&
				Validate(b) &&
				Validate(c) && c.Text != initDate.ToString("d") &&
				Validate(d) &&
				Validate(e) &&
				Validate(f) ) {
				var btn = FindViewById<Button> (Resource.Id.next);
				btn.Enabled = true;
				btn.SetBackgroundColor (Android.Graphics.Color.Rgb(255,237,0));
				btn.SetTextColor (Android.Graphics.Color.Black);
			} else if(x.Enabled) {
				x.Enabled = false;
				x.SetBackgroundColor (Android.Graphics.Color.Rgb (225, 225, 225));
				x.SetTextColor (Android.Graphics.Color.Rgb (175, 175, 175));
			}
		}

		public void UpdateDisplay(){
			var birthdate = FindViewById<EditText> (Resource.Id.birthdate);	// Date Field
			birthdate.Text = currentDate.ToString("d");
		}

		public void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e){
			this.currentDate = e.Date;
			UpdateDisplay ();
		}

		protected override Dialog OnCreateDialog (int id){
			switch (id) {
			case DATE_DIALOG_ID:
				return new DatePickerDialog (this, OnDateSet, currentDate.Year, currentDate.Month - 1, currentDate.Day); 
			}
			return null;
		}

		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Form_1);
			this.initDate = currentDate;

		// Variables
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var name = FindViewById<EditText> (Resource.Id.name); 			// Name Field
			var lastname = FindViewById<EditText> (Resource.Id.lastname); 	// Name Field
			var next = FindViewById<Button> (Resource.Id.next);				// Next Button
			var birthdate = FindViewById<EditText> (Resource.Id.birthdate);	// Date Field
			var street = FindViewById<EditText> (Resource.Id.street);
			var streetNum = FindViewById<EditText> (Resource.Id.streetNum);
			var province = FindViewById<AutoCompleteTextView>(Resource.Id.province);

			// Province Autocomplete
			// TODO: optimize below into strings file with more completeness
			var autoCompleteOptions = new String[] { "Mechelen", "Muizen", "Antwerpen", "Rijmenam", "Leuven", "Gent", "Brugge", "Hever", "Mettet", "Luik", "Brussel" };
			ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, autoCompleteOptions);


		// Toolbar
			// Populate
			toolbar.Title = Resources.GetString(Resource.String.app_name);
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);

			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

		// UI
			// Populate
			UpdateDisplay();
			province.Adapter = autoCompleteAdapter;
			// Style

			// Disable button by default
			next.Enabled = true; // DEBUG - switch to false
			next.SetBackgroundColor (Android.Graphics.Color.Rgb(225,225,225));
			next.SetTextColor (Android.Graphics.Color.Rgb(175,175,175));

		// Events
			// Next Button
			next.Click += delegate {
				var strCharityCountry = Intent.GetStringExtra("strCharityCountry");
				var strVolunteerName = Intent.GetStringExtra("strVolunteerName");
				var strDonatorName = name.EditableText.ToString();
				var strDonatorLastname = lastname.EditableText.ToString();
				var strDonatorBirthdate = birthdate.EditableText.ToString();
				var strDonatorStreet = street.EditableText.ToString();
				var strDonatorStreetNum = streetNum.EditableText.ToString();
				var strDonatorProvince = province.EditableText.ToString();

				var newIntent = new Intent (this, typeof(Form_2));

				newIntent.PutExtra("strVolunteerName",strVolunteerName);
				newIntent.PutExtra("strCharityCountry",strCharityCountry);

				newIntent.PutExtra ("strDonatorName", strDonatorName);
				newIntent.PutExtra ("strDonatorLastname", strDonatorLastname);
				newIntent.PutExtra ("strDonatorBirthdate", strDonatorBirthdate);
				newIntent.PutExtra ("strDonatorStreet", strDonatorStreet);
				newIntent.PutExtra ("intDonatorStreetNum", strDonatorStreetNum);
				newIntent.PutExtra ("strDonatorProvince", strDonatorProvince);
				
				StartActivity (newIntent);	
			};

			// Name Field Validation
			name.TextChanged += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			name.FocusChange += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			// Lastname Field Validation
			lastname.TextChanged += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			lastname.FocusChange += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			// Birthdate Field Validation
			birthdate.Click += delegate {				// trigger on click
				ShowDialog (DATE_DIALOG_ID);
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};
			birthdate.FocusChange += delegate {			// trigger on initial select
				if(birthdate.HasFocus){
					ShowDialog (DATE_DIALOG_ID);
				}
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};
			birthdate.TextChanged += delegate {			// trigger after date chosen from datepicker
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			// Street Field Validation
			street.TextChanged += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			street.FocusChange += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			// Street Number Field Validation
			streetNum.TextChanged += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			streetNum.FocusChange += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			// Province Field Validation
			province.TextChanged += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};

			province.FocusChange += delegate {
				Activate(next, name,lastname,birthdate,street,streetNum,province);
			};
		}
	}
}