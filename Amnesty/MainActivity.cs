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

using supLib = Android.Support.V7;
using supAppCompat = Android.Support.V7.AppCompat;
using supToolbar = Android.Support.V7.Widget.Toolbar;
using supDesign = Android.Support.Design;

namespace Amnesty
{
	[Activity (Label = "Amnesties", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
	public class MainActivity : Activity
	{

		// Function that takes a given EditText element and determines its validation through boolean output
		public Boolean Validate(EditText obj){
			if (String.IsNullOrWhiteSpace (obj.Text)) { // If no text or solely whitespaces are present
				obj.Error = Resources.GetString (Resource.String.error_empty);
				return false;
			} else if (obj.Text.Length <= 2) { // If the input is shorter than 3
				obj.Error = Resources.GetString (Resource.String.error_length);
				return false;
			} else { // Everything's fine
				return true;
			}
		}

		// Function that takes a button and an array of EditText elements and validates each one by the above function
		public void Activate(Button button, EditText[] controls){
			for (int i = 0; i < controls.Length; i++) {// run through all the controls
				if(!Validate(controls[i])){ // check for false first
					button.Enabled = false;
					button.SetBackgroundColor (Android.Graphics.Color.Rgb (225, 225, 225));
					button.SetTextColor (Android.Graphics.Color.Rgb (175, 175, 175));
					break; // Whenever a field fails to validate stop checking all the other fields. Performance & Bugfix
				}else{
					button.Enabled = true;
					button.SetBackgroundColor (Android.Graphics.Color.Rgb(255,237,0));
					button.SetTextColor (Android.Graphics.Color.Black);
				}
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);

		// Variables
			// Views
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var username = FindViewById<EditText> (Resource.Id.username); 	// Username Field
			var password = FindViewById<EditText> (Resource.Id.password); 	// Password Field
			var submit = FindViewById<Button> (Resource.Id.submit); 		// Submit Button

			// Processing
			EditText[] controls = new EditText[] {
				username,password
			};

			// Convenience
			var black = Android.Graphics.Color.Black;
			var gray = Android.Graphics.Color.Rgb (225, 225, 225);
			var grey = Android.Graphics.Color.Rgb (175, 175, 175);

		// UI
			// Populate
			toolbar.Title = Resources.GetString(Resource.String.app_name);

			// Styling
			toolbar.SetTitleTextColor (black);
			submit.SetBackgroundColor (gray);
			submit.SetTextColor (grey);

			// Controls
			password.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;
			submit.Enabled = false;
			toolbar.Menu.Add (Resource.String.generic_cancel);
			toolbar.SetNavigationIcon (Resource.Mipmap.ic_heart_black_36dp);

		// Events
			// Submit - Click
			submit.Click += delegate {
				var newIntent = new Intent (this, typeof(Landing));
				newIntent.PutExtra ("strVolunteerName", username.EditableText.ToString());
				StartActivity (newIntent);
			};

			// Username
			username.TextChanged += delegate {
				Activate(submit, controls);
			};

			username.FocusChange += delegate {
				Activate(submit, controls);
			};

			// Password
			password.TextChanged += delegate {
				Activate(submit, controls);
			};

			password.FocusChange += delegate {
				Activate(submit, controls);
			};
		// End
		}
	}
}