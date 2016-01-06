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
using supDesign = Android.Support.Design;

namespace Amnesty
{
	[Activity (Label = "Amnesties", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		// Variables
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 	// Toolbar
			var username = FindViewById<EditText> (Resource.Id.username); 	// Username Field
			var password = FindViewById<EditText> (Resource.Id.password); 	// Password Field
			var submit = FindViewById<Button> (Resource.Id.submit); 		// Submit Button
			var fab = FindViewById<ImageButton> (Resource.Id.mainButton); 	// Floating Action Button

		// Toolbar
			// Populate
			toolbar.Title = "Amnesties";
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);
			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

		// Fields
			// Styling
			password.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;

		// Events
			// Submit - Click
			submit.Click += delegate {
				var landing = new Intent (this, typeof(Landing));
				landing.PutExtra ("username", username.EditableText.ToString());
				StartActivity (landing);
			};

		}
	}
}


