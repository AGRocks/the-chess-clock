using System;
using Android.Media;
using Xamarin.Forms;
using Android.Content;
using Android.Content.Res;

[assembly: Dependency (typeof(the_chess_clock.Droid.AndriodMediaPlayer))]
namespace the_chess_clock.Droid
{
	public class AndriodMediaPlayer : IMediaPlayer, IDisposable
	{
		private readonly MediaPlayer player;

		public AndriodMediaPlayer ()
		{
			player = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.clap);
		}

		public void PlayClick ()
		{
			try {
				player.Start ();
			} catch (Exception ex) {
				Console.Out.WriteLine (ex.StackTrace);
				throw;
			}
		}		

		public void Dispose ()
		{
			this.player.Release ();
		}
	}
}

