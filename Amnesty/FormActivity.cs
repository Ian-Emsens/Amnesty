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
	[Activity (Label = "Form")]			
	public class Form : Activity
	{
		const int DATE_DIALOG_ID = 0;
		DateTime currentDate = DateTime.Today.AddYears(-18);
		DateTime initDate;

		public Boolean Validate(EditText obj){
			if (String.IsNullOrWhiteSpace(obj.Text)) {
				obj.Error = Resources.GetString(Resource.String.error_empty);
				return false;
			} else {
				return true;
			}
		}

		public void Activate(EditText a, EditText b, EditText c){
			if (Validate(a) && Validate(b) && Validate(c) && c.Text != initDate.ToString("d")) {
				var btn = FindViewById<Button> (Resource.Id.next);
				btn.Enabled = true;
				btn.SetBackgroundColor (Android.Graphics.Color.Rgb(255,237,0));
				btn.SetTextColor (Android.Graphics.Color.Black);
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
			SetContentView (Resource.Layout.Form_Personal);
			this.initDate = currentDate;

		// Variables
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var name = FindViewById<EditText> (Resource.Id.name); 			// Name Field
			var lastname = FindViewById<EditText> (Resource.Id.lastname); 	// Name Field
			var next = FindViewById<Button> (Resource.Id.next);				// Next Button
			var birthdate = FindViewById<EditText> (Resource.Id.birthdate);	// Date Field

		// Toolbar
			// Populate
			toolbar.Title = Resources.GetString(Resource.String.app_name);
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);

			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

		// UI
			// Populate
			UpdateDisplay();
			// Disable button by default
			next.Enabled = true; // DEBUG - switch to false
			next.SetBackgroundColor (Android.Graphics.Color.Rgb(225,225,225));
			next.SetTextColor (Android.Graphics.Color.Rgb(175,175,175));

		// Events
			// Birthdate Field Validation
			birthdate.Click += delegate {				// trigger on click
				ShowDialog (DATE_DIALOG_ID);
				Activate(name,lastname,birthdate);
			};
			birthdate.FocusChange += delegate {			// trigger on initial trigger
				if(birthdate.HasFocus){
					ShowDialog (DATE_DIALOG_ID);
				}
				Activate(name,lastname,birthdate);
			};
			birthdate.TextChanged += delegate {			// trigger after date chosen from datepicker
				Activate(name,lastname,birthdate);
			};

			// Username Field Validation
			name.TextChanged += delegate {
				Activate(name,lastname,birthdate);
			};

			name.FocusChange += delegate {
				Activate(name,lastname,birthdate);
			};

			// Password Field Validation
			lastname.TextChanged += delegate {
				Activate(name,lastname,birthdate);
			};

			lastname.FocusChange += delegate {
				Activate(name,lastname,birthdate);
			};
		}
	}
}