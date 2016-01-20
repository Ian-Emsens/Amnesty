using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using supAppCompat = Android.Support.V7.AppCompat;
using supToolbar = Android.Support.V7.Widget.Toolbar;
using supFAB = Android.Support.Design.Widget.FloatingActionButton;
using supDesign = Android.Support.Design;

namespace Amnesty
{
	public class actions : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
			// ain't shit works here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		// Variables
			var view = inflater.Inflate(Resource.Layout.actions, container, false);					// inflate the view and load our layout
			var t = this.Activity;

			var fab = view.FindViewById<supFAB> (Resource.Id.mainButton);							// main FAB
			var fabActionNew = view.FindViewById<supFAB> (Resource.Id.subAction_1);					// sub FAB 1
			var fabActionMiss = view.FindViewById<supFAB> (Resource.Id.subAction_2);				// sub FAB 2
			var actionOverlay = view.FindViewById<RelativeLayout> (Resource.Id.actionOverlay);		// bounding box
			var actionContainer = view.FindViewById<RelativeLayout> (Resource.Id.actionContainer);	// sub FAB container

			// Convenience
			var pad = 28;

		// UI
			// sub FAB's
			fabActionNew.SetPadding(pad,pad,pad,pad);
			fabActionMiss.SetPadding(pad,pad,pad,pad);

		// Events
			// main FAB
			fab.Click += delegate {
				// check if button is activated
				if (!fab.Activated) {
				// Styling
					actionOverlay.SetBackgroundColor (Android.Graphics.Color.Argb (180, 0, 0, 0));
					actionContainer.Visibility = Android.Views.ViewStates.Visible;

				// Animations
					// slideIn animation
					var slide = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.abc_slide_in_bottom);
					// + -> x animation
					var anim = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.rotate_45);
					anim.FillAfter = true; //keep endState of animation
					// fade animation
					var fade = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.abc_fade_in);
					fade.FillAfter = true;

					// trigger animations
					fab.StartAnimation (anim);
					actionContainer.StartAnimation (slide);
					actionOverlay.StartAnimation (fade);

					//change state
					fab.Activated = true;
				} else {
				// Styling
					actionContainer.Visibility = Android.Views.ViewStates.Gone;

				// Animations
					// slideOut animation
					var slide = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.abc_slide_out_bottom);
					// x -> + animation
					var anim = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.rotate_m45);
					anim.FillAfter = true; //keep endState of animation
					// fade animation
					var fade = Android.Views.Animations.AnimationUtils.LoadAnimation (t, Resource.Animation.abc_fade_out);
					fade.FillAfter = true;

					//trigger animations
					fab.StartAnimation (anim);
					actionContainer.StartAnimation (slide);
					actionOverlay.StartAnimation (fade);

					//change state
					fab.Activated = false;
				}
			};

			return view;
		}
	}
}

