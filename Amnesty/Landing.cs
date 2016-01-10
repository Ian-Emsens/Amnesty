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
	[Activity (Label = "Landing")]			
	public class Landing : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			this.Window.AddFlags(WindowManagerFlags.Fullscreen);

			// Set our view from the "landing" layout resource
			SetContentView (Resource.Layout.Landing);

		// Variables
			// UI
			var currentUser = FindViewById<TextView> (Resource.Id.currentUser);	// Current User
			var fab = FindViewById<supFAB> (Resource.Id.mainButton);
			// Paragraphs
			var p1 = FindViewById<TextView> (Resource.Id.paragraph_01);
			var p2 = FindViewById<TextView> (Resource.Id.paragraph_02);
			var p3 = FindViewById<TextView> (Resource.Id.paragraph_03);
			var p4 = FindViewById<TextView> (Resource.Id.paragraph_04);
			var p5 = FindViewById<TextView> (Resource.Id.paragraph_05);

		// Events
			fab.Click += delegate {
				currentUser.Text = "tapped son";
			};

		// Content
			// Optimize: check string files of occurences of 'yemen_' and insert & populate X number in view
			p1.Text = Resources.GetString(Resource.String.yemen_01);
			p2.Text = Resources.GetString(Resource.String.yemen_02);
			p3.Text = Resources.GetString(Resource.String.yemen_03);
			p4.Text = Resources.GetString(Resource.String.yemen_04);
			p5.Text = Resources.GetString(Resource.String.yemen_05);

		// DEBUG
			currentUser.Text = Resources.GetString(Resource.String.generic_hi) + " " + Intent.GetStringExtra("username");

		}
	}
}

