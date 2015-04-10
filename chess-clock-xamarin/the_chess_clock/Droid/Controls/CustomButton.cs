using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Widget;
using Xamarin.Forms;
using the_chess_clock.Droid;
using Android.Content.Res;

[assembly: ExportRenderer (typeof(Xamarin.Forms.Button), typeof(MyButtonRenderer))]
namespace  the_chess_clock.Droid
{
	public class MyButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged (e);
			var label = (TextView)Control; // for example
			Typeface font = Typeface.CreateFromAsset (Forms.Context.Assets, "LeagueGothic-Regular.otf");
			label.TextSize = 100; // gets smaller if it doesn't fit
			label.Typeface = font;
		}
	}
}

