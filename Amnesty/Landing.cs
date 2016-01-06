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
	[Activity (Label = "Landing")]			
	public class Landing : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "landing" layout resource
			SetContentView (Resource.Layout.Landing);

			// Variables
			var currentUser = FindViewById<TextView> (Resource.Id.currentUser);	// Current User
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar); 		// Toolbar

			// Toolbar
			// Populate
			toolbar.Title = "Amnesties";
			toolbar.SetLogo (Resource.Drawable.logo_black_trans_xs);
			// Styling
			toolbar.SetTitleTextColor (Android.Graphics.Color.Black);

			// Events
			currentUser.Text = Intent.GetStringExtra("username");
		}
	}
}

