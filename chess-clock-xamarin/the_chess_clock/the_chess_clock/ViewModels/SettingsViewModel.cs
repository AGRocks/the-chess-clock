using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

[assembly: Dependency (typeof(the_chess_clock.SettingsViewModel))]
namespace the_chess_clock
{
	public class SettingsViewModel : ViewModelBase
	{
		private const string BLACK_TIME = "blackTime";
		private const string WHITE_TIME = "whiteTIme";
		private const string INCREMENTAL = "incremental";
		private const string REVIEWED = "reviewed";
		private const string USAGES = "usages";

		private ISetttingsProvider settings;
		private RelayCommand addSecondCommand;
		private RelayCommand substractSecondCommand;

		public SettingsViewModel()
		{
			this.settings = DependencyService.Get<ISetttingsProvider> ();
		}

		private int GetIntProperty(string key, int defaultValue)
		{
			var value = this.settings[key];
			return !string.IsNullOrEmpty(value) ? int.Parse(value) : defaultValue;
		}

		public TimeSpan WhiteTime
		{
			get 
			{
				var defaultValue = (int) TimeSpan.FromMinutes (5).TotalMilliseconds;
				return TimeSpan.FromMilliseconds (this.GetIntProperty (WHITE_TIME, defaultValue));
			}

			set
			{
				this.settings[WHITE_TIME] = value.TotalMilliseconds.ToString();
				this.RaisePropertyChanged("WhiteTime");
			}
		}

		public TimeSpan BlackTime
		{
			get
			{
				var defaultValue = (int) TimeSpan.FromMinutes (5).TotalMilliseconds;
				return TimeSpan.FromMilliseconds (this.GetIntProperty (BLACK_TIME, defaultValue));
			}

			set
			{
				this.settings [BLACK_TIME] = value.TotalMilliseconds.ToString ();
				this.RaisePropertyChanged("BlackTime");
			}
		}

		public int Incremental
		{
			get
			{
				return this.GetIntProperty (INCREMENTAL, 0);
			}

			set
			{
				if (value >= 0)
				{
					this.settings [INCREMENTAL] = value.ToString ();
					this.RaisePropertyChanged("Incremental");
				}
			}
		}

		public bool Reviewed
		{
			get
			{
				return this.GetIntProperty (REVIEWED, 0) == 1;
			}

			set
			{
				if (value)
					this.settings [REVIEWED] = "1";
				else
					this.settings [REVIEWED] = "0";
				this.RaisePropertyChanged("Reviewed");
			}
		}

		public int Usages
		{
			get
			{
				return this.GetIntProperty (USAGES, 0);
			}

			set
			{
				if (value >= 0)
				{
					this.settings [USAGES] = value.ToString();
					this.RaisePropertyChanged("Usages");
				}
			}
		}

		public ICommand AddSecondCommand
		{
			get
			{
				if (this.addSecondCommand == null)
				{
					this.addSecondCommand = new RelayCommand(() => Incremental++ );
				}

				return this.addSecondCommand;
			}
		}

		public ICommand SubstractSecondCommand
		{
			get
			{
				if (this.substractSecondCommand == null)
				{
					this.substractSecondCommand = new RelayCommand(() => Incremental--);
				}

				return this.substractSecondCommand;
			}
		}

		public List<int> IncrementalRange
		{
			get
			{
				return Enumerable.Range(0, 59).ToList();
			}
		}

		public void Save()
		{
			this.settings.Save ();
		}
	}
}

