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
using supEditText = Android.Support.Design.Widget.TextInputLayout;
using supDesign = Android.Support.Design;
using DrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using NavigationView = Android.Support.Design.Widget.NavigationView;

using InputMethodManager= Android.Views.InputMethods.InputMethodManager;

namespace Amnesty
{
	[Activity (Label = "Form_1")]			
	public class Form_1 : Activity
	{
		// START DATEPICKER
		//

		const int DATE_DIALOG_ID = 0;
		DateTime currentDate = DateTime.Now;

		public void UpdateDisplay(){
			var birthdate = FindViewById<supEditText> (Resource.Id.birthdate);	// Date Field
			birthdate.EditText.Text = currentDate.ToString("d");
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

		//
		// END DATEPICKER

		// START VALIDATOR
		//

		// Function that takes a given EditText element and determines its validation through boolean output
		public Boolean Validate(supEditText obj){
			if (String.IsNullOrWhiteSpace (obj.EditText.Text)) { // If no text or solely whitespaces are present
				obj.ErrorEnabled = true;
				obj.Error = Resources.GetString (Resource.String.error_empty);
				return false;
			} else { // Everything's fine
				obj.ErrorEnabled = false;
				obj.Error = null;
				return true;
			}
		}

		// Function that takes a button and an array of EditText elements and validates each one by the above function
		public void Activate(Button button, supEditText[] controls){
			for (int i = 0; i < controls.Length; i++) {// run through all the controls
				if(!Validate(controls[i])){ // check for false first
					if (button.Enabled) {
						button.Enabled = false;
						button.SetBackgroundColor (Android.Graphics.Color.Rgb (225, 225, 225));
						button.SetTextColor (Android.Graphics.Color.Rgb (175, 175, 175));
						break; // Disabled the button, no need to keep looping
					}
					break; // Whenever a field fails to validate stop checking all the other fields. Performance & Bugfix
				}else{
					button.Enabled = true;
					button.SetBackgroundColor (Android.Graphics.Color.Rgb(255,237,0));
					button.SetTextColor (Android.Graphics.Color.Black);
				}
			}
		}

		//
		// END VALIDATOR

		public void hideKeyboard(EditText previousField){
			
		}

		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Form_1);

		// Variables
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 				// Toolbar
			var next = FindViewById<Button> (Resource.Id.next);							// Next Button
			var name = FindViewById<supEditText> (Resource.Id.name); 					// Name Field
			var lastname = FindViewById<supEditText> (Resource.Id.lastname); 			// Name Field
			var birthdate = FindViewById<supEditText> (Resource.Id.birthdate);			// Date Field
			var street = FindViewById<AutoCompleteTextView> (Resource.Id.street);		// Autocomplete target Field
			var streetP = FindViewById<supEditText> (Resource.Id.streetP);				// Street Field
			var streetNum = FindViewById<supEditText> (Resource.Id.streetNum);			// Number Field
			var province = FindViewById<AutoCompleteTextView>(Resource.Id.province);	// Autocomplete target Field
			var provinceP = FindViewById<supEditText> (Resource.Id.provinceP);			// Province Field

			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);

			// Processing
			supEditText[] controls = new supEditText[] {
				name,lastname,birthdate,streetP,streetNum,provinceP
			};

		// Configuration
			// TODO: optimize below into strings file with more completeness
			var autoCompleteProvinceOptions = new String[] { "Mechelen", "Muizen", "Antwerpen", "Rijmenam", "Leuven", "Gent", "Brugge", "Hever", "Mettet", "Luik", "Brussel" };
			ArrayAdapter autoCompleteProvinceAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, autoCompleteProvinceOptions);

			var autoCompleteStreetOptions = new String[] { "Stationsstraat", "Kerkstraat", "Veldweg", "Slagersstraat", "Kapelstraat" };
			ArrayAdapter autoCompleteStreetAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, autoCompleteStreetOptions);

		// UI
			// Populate
			UpdateDisplay();
			toolbar.Title = Resources.GetString(Resource.String.app_name);
			province.Adapter = autoCompleteProvinceAdapter;
			street.Adapter = autoCompleteStreetAdapter;
			// Style
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

			// Disable button by default
			next.Enabled = true; // DEBUG - switch to false
			next.SetBackgroundColor (Android.Graphics.Color.Rgb(225,225,225));
			next.SetTextColor (Android.Graphics.Color.Rgb(175,175,175));

		// Events
			// Next Button
			next.Click += delegate {
				var strCharityCountry = Intent.GetStringExtra("strCharityCountry");
				var strVolunteerName = Intent.GetStringExtra("strVolunteerName");

				var newIntent = new Intent (this, typeof(Form_2));

				newIntent.PutExtra("strVolunteerName",strVolunteerName);
				newIntent.PutExtra("strCharityCountry",strCharityCountry);

				newIntent.PutExtra ("strDonatorName", name.EditText.EditableText.ToString());
				newIntent.PutExtra ("strDonatorLastname", lastname.EditText.EditableText.ToString());
				newIntent.PutExtra ("strDonatorBirthdate", birthdate.EditText.EditableText.ToString());
				newIntent.PutExtra ("strDonatorStreet", street.EditableText.ToString());
				newIntent.PutExtra ("strDonatorStreetNum", streetNum.EditText.EditableText.ToString());
				newIntent.PutExtra ("strDonatorProvince", province.EditableText.ToString());
				
				StartActivity (newIntent);	
			};

			// Name Field Validation
			name.EditText.TextChanged += delegate {
				Activate(next, controls);
			};

			name.FocusChange += delegate {
				Activate(next, controls);
			};

			// Lastname Field Validation
			lastname.EditText.TextChanged += delegate {
				Activate(next, controls);
			};

			lastname.FocusChange += delegate {
				Activate(next, controls);
			};

			// Birthdate Field Validation
			birthdate.EditText.FocusChange += delegate {
				if(birthdate.EditText.HasFocus){
					imm.HideSoftInputFromWindow(birthdate.EditText.WindowToken, 0);
					ShowDialog (DATE_DIALOG_ID);
				}
			};

			birthdate.FocusChange += delegate {			// trigger on initial select
				Activate(next, controls);
			};

			birthdate.EditText.TextChanged += delegate {			// trigger after date chosen from datepicker
				Activate(next, controls);
				street.RequestFocus();

			};

			// Street Field Validation
			streetP.EditText.TextChanged += delegate {
				Activate(next, controls);
			};

			streetP.FocusChange += delegate {
				imm.ShowSoftInput(street,Android.Views.InputMethods.ShowFlags.Forced);
				Activate(next, controls);
			};

			// Street Number Field Validation
			streetNum.EditText.TextChanged += delegate {
				Activate(next, controls);
			};

			streetNum.FocusChange += delegate {
				Activate(next, controls);
			};

			// Province Field Validation
			provinceP.EditText.TextChanged += delegate {
				Activate(next, controls);
			};

			provinceP.EditText.FocusChange += delegate {
				Activate(next, controls);
			};
		// Menu Psuedo-fragment
			// Variables
			DrawerLayout drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			NavigationView navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
			var navHeader = navigationView.InflateHeaderView (Resource.Menu.header);

			// Populate
			toolbar.NavigationIcon = Resources.GetDrawable (Resource.Mipmap.ic_menu_black_24dp);

			// Events
			toolbar.NavigationClick += delegate {
				drawerLayout.OpenDrawer (Android.Support.V4.View.GravityCompat.Start);
			};

			drawerLayout.DrawerOpened += delegate {
				imm.HideSoftInputFromWindow(birthdate.EditText.WindowToken, 0);
			};

			// Populate the username once we're sure the header has been inflated
			navHeader.ViewAttachedToWindow += delegate {
				var navUsername = FindViewById<TextView> (Resource.Id.nav_username);
				navUsername.Text = Intent.GetStringExtra ("strVolunteerName") ?? "DEBUG MODE";
			};

			navigationView.NavigationItemSelected += (sender, e) => {
				e.MenuItem.SetChecked (true);
				Console.WriteLine(e.MenuItem.TitleFormatted.ToString());
				drawerLayout.CloseDrawers ();
			};
		}
	}
}