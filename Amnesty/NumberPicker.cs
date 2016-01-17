
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

namespace Amnesty 	
{
	public class NumberPickerFragment : DialogFragment 
	{

		public Dialog onCreateDialog(Bundle savedInstanceState) {
			Console.WriteLine ("atleast I did something");
			return new Dialog(this.Activity);
		}

		public void onSelect(ProgressDialog view) {
			// Do something with the time chosen by the user
		}
	}
}

