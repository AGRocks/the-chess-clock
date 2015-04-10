using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace the_chess_clock
{
	public partial class Clock : ContentPage
	{
		public Clock ()
		{
			InitializeComponent ();
			this.BindingContext = DependencyService.Get<ClockViewModel> ();
		}

		public void OnSettingsClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync (new Settings ());
		}
	}
}

