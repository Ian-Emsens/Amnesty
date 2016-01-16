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
			var toolbar = FindViewById<supToolbar> (Resource.Id.toolbar);
			var scrollview = FindViewById<ScrollView> (Resource.Id.scrollview);
			var container = FindViewById<RelativeLayout> (Resource.Id.container);

			// Content
			var title = FindViewById<TextView> (Resource.Id.title);
			var desc = FindViewById<TextView> (Resource.Id.description);
			var p1 = FindViewById<TextView> (Resource.Id.paragraph_01);
			var p2 = FindViewById<TextView> (Resource.Id.paragraph_02);
			var p3 = FindViewById<TextView> (Resource.Id.paragraph_03);
			var p4 = FindViewById<TextView> (Resource.Id.paragraph_04);
			var p5 = FindViewById<TextView> (Resource.Id.paragraph_05);

			// Fragment UI - Amnesty.actions.cs
			var fabActionNew = FindViewById<supFAB> (Resource.Id.subAction_1);

		// UI
			// Populate
			toolbar.Menu.Add (Resource.String.generic_cancel);

			// Styling
			toolbar.OverflowIcon = Resources.GetDrawable (Resource.Mipmap.ic_dots_vertical_black_24dp);
			toolbar.SetBackgroundColor (Android.Graphics.Color.Transparent);

		// Content
			// TODO: check string files of occurences of 'yemen_' and insert & populate X number in view
			title.Text = Resources.GetString(Resource.String.yemen_title);
			desc.Text = Resources.GetString (Resource.String.yemen_desc);
			p1.Text = Resources.GetString(Resource.String.yemen_01);
			p2.Text = Resources.GetString(Resource.String.yemen_02);
			p3.Text = Resources.GetString(Resource.String.yemen_03);
			p4.Text = Resources.GetString(Resource.String.yemen_04);
			p5.Text = Resources.GetString(Resource.String.yemen_05);

		// Events
			//New Donation
			fabActionNew.Click += delegate {
				var strCharityCountry = title.Text.ToString ();
				var strVolunteerName = Intent.GetStringExtra ("strVolunteerName");

				var newIntent = new Intent (this, typeof(Form_1));

				newIntent.PutExtra ("strVolunteerName", strVolunteerName);
				newIntent.PutExtra ("strCharityCountry", strCharityCountry);

				StartActivity (newIntent);
			};

			title.LongClick += delegate {
				PopupMenu menu = new PopupMenu (this, title);
				menu.Inflate(Resource.Menu.logoutMenu);
				menu.MenuItemClick += delegate(object sender, PopupMenu.MenuItemClickEventArgs e) {
					Console.WriteLine(e.Item.TitleFormatted);
				};
				menu.Show();
			};

			// End
		}
	}
}