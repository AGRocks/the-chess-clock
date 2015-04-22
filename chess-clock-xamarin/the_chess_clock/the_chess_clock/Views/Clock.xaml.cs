using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace the_chess_clock
{
	public partial class Clock : ContentPage
	{
		IMediaPlayer player;

		public Clock ()
		{
			InitializeComponent ();
			this.BindingContext = DependencyService.Get<ClockViewModel> ();
			this.player = DependencyService.Get<IMediaPlayer> (DependencyFetchTarget.GlobalInstance);
			this.WhiteButton.Clicked +=	OnButtonClicked;
			this.BlackButton.Clicked +=	OnButtonClicked;
		}

		public void OnButtonClicked (object sender, EventArgs e)
		{
			player.PlayClick ();
		}

		public void OnSettingsClicked (object sender, EventArgs e)
		{
			this.Navigation.PushAsync (new Settings ());
		}
	}
}

