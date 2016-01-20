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

using Android.Support.V7;

using supAppCompat = Android.Support.V7.AppCompat;
using supToolbar = Android.Support.V7.Widget.Toolbar;
using supFAB = Android.Support.Design.Widget.FloatingActionButton;
using supDesign = Android.Support.Design;
using DrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using NavigationView = Android.Support.Design.Widget.NavigationView;

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
			var mcContainer = FindViewById<LinearLayout> (Resource.Id.main_content_container);

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

			// Positioning
			mcContainer.SetPadding(0,toolbar.MinimumHeight,0,0);

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
			// New Donation
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
		// Menu Psuedo-fragment
			// Variables
			DrawerLayout drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			NavigationView navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
			var navHeader = navigationView.InflateHeaderView (Resource.Menu.header);

			// Styling
			navigationView.SetItemTextAppearance();

			// Populate
			toolbar.NavigationIcon = Resources.GetDrawable (Resource.Mipmap.ic_menu_black_24dp);

			// Events
			toolbar.NavigationClick += delegate {
				drawerLayout.OpenDrawer (Android.Support.V4.View.GravityCompat.Start);
			};

			// Populate the menu once we're sure the header has been inflated
			navHeader.ViewAttachedToWindow += delegate {
				var navUsername = FindViewById<TextView> (Resource.Id.nav_username);
				navUsername.Text = Intent.GetStringExtra ("strVolunteerName") ?? "DEBUG MODE";
			};

			drawerLayout.ViewAttachedToWindow += delegate {
				navigationView.SetCheckedItem (Resource.Id.nav_home);
				navigationView.SetCheckedItem (Resource.Id.nav_sub_yemen);
			};

			navigationView.NavigationItemSelected += (sender, e) => {
				Console.WriteLine(e.MenuItem.TitleFormatted.ToString());
				drawerLayout.CloseDrawers ();
			};

			// End
		}
	}
}