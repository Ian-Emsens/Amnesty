using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Amnesty
{
	[Activity (Label = "Amnesty", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

			//Toolbar will now take on default actionbar characteristics
			//ActionBar = new ActionBar (toolbar);

			toolbar.Title = "Hello from Appcompat Toolbar";
			//..
		}
	}
}


