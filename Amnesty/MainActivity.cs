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
using supEditText = Android.Support.Design.Widget.TextInputLayout;
using supDesign = Android.Support.Design;

namespace Amnesty
{
	[Activity (Label = "Amnesties", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
	public class MainActivity : Activity
	{

		// Function that takes a given EditText element and determines its validation through boolean output
		public Boolean Validate(supEditText obj){
			if (String.IsNullOrWhiteSpace (obj.EditText.Text)) { // If no text or solely whitespaces are present
				obj.ErrorEnabled = true;
				obj.Error = Resources.GetString (Resource.String.error_empty);
				return false;
			} else if (obj.EditText.Text.Length <= 2) { // If the input is shorter than 3
				obj.ErrorEnabled = true;
				obj.Error = Resources.GetString (Resource.String.error_length);
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

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);

		// Variables
			// Views
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar);
			var username = FindViewById<supEditText> (Resource.Id.username);
			var password = FindViewById<supEditText> (Resource.Id.password);
			var submit = FindViewById<Button> (Resource.Id.submit);

			// Derivatives
			var usernameInput = username.EditText;
			var passwordInput = password.EditText;

			// Processing
			supEditText[] controls = new supEditText[] {
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

			// Controls
			passwordInput.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;

			// Testing
			toolbar.Menu.Add (Resource.String.generic_cancel);
			toolbar.OverflowIcon = Resources.GetDrawable(Resource.Mipmap.ic_dots_vertical_black_24dp);

		// Events
			// Submit - Click
			submit.Click += delegate {
				var newIntent = new Intent (this, typeof(Landing));
				newIntent.PutExtra ("strVolunteerName", usernameInput.EditableText.ToString());
				StartActivity (newIntent);
			};

			// Username
			usernameInput.TextChanged += delegate {
				if(usernameInput.HasFocus){
					Activate(submit, controls);
				}
			};

			// Password
			passwordInput.TextChanged += delegate {
				if(passwordInput.HasFocus){
					Activate(submit, controls);
				}
			};

			// Testing
			toolbar.Click += delegate {
				DialogFragment newFragment = new NumberPickerFragment();
				newFragment.Show(FragmentManager,"timePicker");
			};
		// End
		}

		protected override void OnStart ()
		{
			base.OnStart();

		// Convenience
			var gray = Android.Graphics.Color.Rgb (225, 225, 225);
			var grey = Android.Graphics.Color.Rgb (175, 175, 175);

		// Populate
			var submit = FindViewById<Button> (Resource.Id.submit);
			submit.SetBackgroundColor (gray);
			submit.SetTextColor (grey);
			submit.Enabled = true; // DEBUG

			var username = FindViewById<supEditText> (Resource.Id.username);
			username.EditText.EditableText.Clear ();
			username.EditText.ClearFocus();

			var password = FindViewById<supEditText> (Resource.Id.password);
			password.EditText.EditableText.Clear ();
			password.EditText.ClearFocus();

			// disable errors after all events have been called to avoid TextChanged firing and showing error on username
			username.ErrorEnabled = false;
			password.ErrorEnabled = false;
		}
	}
}