using System;
using System.Linq;
using System.Reflection;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using XamSvg;

namespace XamSvgTests
{
	[Activity (Label = "Empty !")]
	public class EmptyActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.empty);
		}
	}
}
