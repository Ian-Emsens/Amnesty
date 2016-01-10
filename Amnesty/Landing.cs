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
			var title = FindViewById<TextView> (Resource.Id.title);
			var desc = FindViewById<TextView> (Resource.Id.description);

			var scrollview = FindViewById<ScrollView> (Resource.Id.scrollview);

			var fab = FindViewById<supFAB> (Resource.Id.mainButton);

			var actionOverlay = FindViewById<RelativeLayout> (Resource.Id.actionOverlay);
			var actionContainer = FindViewById<RelativeLayout> (Resource.Id.actionContainer);

			var fabActionNew = FindViewById<supFAB> (Resource.Id.subAction_1);
			var fabActionNewLabel = FindViewById<TextView> (Resource.Id.subAction_1_tag);
			var fabActionMiss = FindViewById<supFAB> (Resource.Id.subAction_2);
			var fabActionMissLabel = FindViewById<TextView> (Resource.Id.subAction_2_tag);

			// Paragraphs
			var p1 = FindViewById<TextView> (Resource.Id.paragraph_01);
			var p2 = FindViewById<TextView> (Resource.Id.paragraph_02);
			var p3 = FindViewById<TextView> (Resource.Id.paragraph_03);
			var p4 = FindViewById<TextView> (Resource.Id.paragraph_04);
			var p5 = FindViewById<TextView> (Resource.Id.paragraph_05);
			// Convenience
			var pad = 28;

		// UI Configuration
			// FABs
			fabActionNew.SetPadding(pad,pad,pad,pad);
			fabActionNewLabel.Text = Resources.GetString (Resource.String.ui_newDonation);
			fabActionMiss.SetPadding(pad,pad,pad,pad);
			fabActionMissLabel.Text = Resources.GetString (Resource.String.ui_miss);


		// Content
			title.Text = Resources.GetString(Resource.String.yemen_title);
			desc.Text = Resources.GetString (Resource.String.yemen_desc);
			// Optimize: check string files of occurences of 'yemen_' and insert & populate X number in view
			p1.Text = Resources.GetString(Resource.String.yemen_01);
			p2.Text = Resources.GetString(Resource.String.yemen_02);
			p3.Text = Resources.GetString(Resource.String.yemen_03);
			p4.Text = Resources.GetString(Resource.String.yemen_04);
			p5.Text = Resources.GetString(Resource.String.yemen_05);

		// Events
			// FAB
			fab.Click += delegate {
				// check if button is activated
				if(!fab.Activated){

					scrollview.CanScrollVertically(0);
					
					actionOverlay.SetBackgroundColor(Android.Graphics.Color.Argb(180,0,0,0));
					actionContainer.Visibility = Android.Views.ViewStates.Visible;

				// Animations
					// slideIn animation
					var slide = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.abc_slide_in_bottom);
					// + -> x animation
					var anim = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_45);
					anim.FillAfter = true;

					// trigger animations
					fab.StartAnimation(anim);
					actionContainer.StartAnimation(slide);

					//change state
					fab.Activated = true;

				}else{

					scrollview.CanScrollVertically(1);
					
					actionOverlay.SetBackgroundColor(Android.Graphics.Color.Argb(0,0,0,0));
					actionContainer.Visibility = Android.Views.ViewStates.Gone;

				// Animations
					// slideOut animation
					var slide = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.abc_slide_out_bottom);
					// + -> x animation
					var anim = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_m45);
					anim.FillAfter = true;

					//trigger animations
					fab.StartAnimation(anim);
					actionContainer.StartAnimation(slide);

					//change state
					fab.Activated = false;

				}
			// End
			};
		}
	}
}