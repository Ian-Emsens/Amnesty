﻿using System;
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
using supDesign = Android.Support.Design;

namespace Amnesty
{
	[Activity (Label = "Amnesties", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
	public class MainActivity : Activity
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

		public void Activate(EditText a, EditText b){
			if (Validate(a) && Validate(b)) {
				var submit = FindViewById<Button> (Resource.Id.submit);
				submit.Enabled = true;
				submit.SetBackgroundColor (Android.Graphics.Color.Rgb(255,237,0));
				submit.SetTextColor (Android.Graphics.Color.Black);
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		// Variables
			// UI
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var username = FindViewById<EditText> (Resource.Id.username); 	// Username Field
			var password = FindViewById<EditText> (Resource.Id.password); 	// Password Field
			var submit = FindViewById<Button> (Resource.Id.submit); 		// Submit Button

		// Toolbar
			// Populate
			toolbar.Title = Resources.GetString(Resource.String.app_name);
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);

			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

		// UI
			// Controls
			password.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;

			// Disable button by default
			submit.Enabled = true; // DEBUG - switch to false
			submit.SetBackgroundColor (Android.Graphics.Color.Rgb(225,225,225));
			submit.SetTextColor (Android.Graphics.Color.Rgb(175,175,175));

		// Events
			// Submit - Click
			submit.Click += delegate {
				var landing = new Intent (this, typeof(Landing));
				landing.PutExtra ("username", username.EditableText.ToString());
				StartActivity (landing);
			};

			// Username Field Validation
			username.TextChanged += delegate {
				Activate(username,password);
			};

			username.FocusChange += delegate {
				Activate(username,password);
			};

			// Password Field Validation
			password.TextChanged += delegate {
				Activate(username,password);
			};

			password.FocusChange += delegate {
				Activate(username,password);
			};
		// End
		}
	}
}