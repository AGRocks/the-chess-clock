using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace the_chess_clock
{
	public partial class Settings : ContentPage
	{
		private SettingsViewModel viewModel;

		public Settings ()
		{
			InitializeComponent ();
			this.BindingContext = this.viewModel = DependencyService.Get<SettingsViewModel> ();
		}

		public void OnSaveSettingsClicked(object sender, EventArgs e)
		{
			this.viewModel.Save ();
			this.Navigation.PushAsync (new Clock ());
		}
	}
}

