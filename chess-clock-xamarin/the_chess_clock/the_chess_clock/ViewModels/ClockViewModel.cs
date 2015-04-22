using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency (typeof(the_chess_clock.ClockViewModel))]
namespace the_chess_clock
{
	public class ClockViewModel : ViewModelBase
	{
		private bool isWhiteMove;
		private bool isBlackMove;
		private TimeSpan whiteTime;
		private TimeSpan blackTime;
		private ICommand whiteMoveCommand;
		private ICommand blackMoveCommand;
		private ICommand resetTimeCommand;
		private ICommand startTimeCommand;
		private bool isRunning;

		private SettingsViewModel appSettings;
		private RelayCommand pauseTimeCommand;

		public ClockViewModel ()
		{
			this.appSettings = new SettingsViewModel ();
			this.whiteTime = this.appSettings.WhiteTime;
			this.blackTime = this.appSettings.BlackTime;
			this.IsWhiteMove = true;
		}

		private bool TimerTick ()
		{
			if (this.IsWhiteMove && this.IsRunning) {
				this.whiteTime = this.whiteTime.Subtract (TimeSpan.FromSeconds (1));
				this.RaisePropertyChanged ("WhiteTime");
				this.RaisePropertyChanged ("WhiteTimeStr");
			}

			if (this.isBlackMove && this.IsRunning) {
				this.blackTime = this.blackTime.Subtract (TimeSpan.FromSeconds (1));
				this.RaisePropertyChanged ("BlackTime");
			}

			this.RaisePropertyChanged ("WhiteTimeElapsed");
			this.RaisePropertyChanged ("BlackTimeElapsed");

			return this.IsRunning;
		}
			
		public bool IsWhiteMove {
			get {
				return this.isWhiteMove;
			}

			set {
				this.isWhiteMove = value;
				this.RaisePropertyChanged ();
			}
		}

		public bool IsBlackMove {
			get {
				return this.isBlackMove;
			}

			set {
				this.isBlackMove = value;
				this.RaisePropertyChanged ();
			}
		}

		public ICommand WhiteMoveCommand {
			get {
				if (this.whiteMoveCommand == null) {
					this.whiteMoveCommand = new RelayCommand (this.OnWhiteMove);
				}

				return this.whiteMoveCommand;
			}
		}

		public ICommand BlackMoveCommand {
			get {
				if (this.blackMoveCommand == null) {
					this.blackMoveCommand = new RelayCommand (this.OnBlackMove);
				}

				return this.blackMoveCommand;
			}
		}

		private void OnBlackMove ()
		{
			if (this.IsBlackMove) {
				this.blackTime = this.blackTime.Add (TimeSpan.FromSeconds (appSettings.Incremental));
				this.IsBlackMove = false;
				this.IsWhiteMove = true;
				this.RaisePropertyChanged ("BlackTime");
				this.StartTimer ();
			}
		}

		private void StartTimer ()
		{
			if (!this.IsRunning)
			{
				this.isRunning = true;
				Xamarin.Forms.Device.StartTimer (TimeSpan.FromSeconds (1), TimerTick);
				this.RaisePropertyChanged ("IsRunning");
			}
		}

		private void OnWhiteMove ()
		{
			if (this.IsWhiteMove) {
				this.whiteTime = this.whiteTime.Add (TimeSpan.FromSeconds (appSettings.Incremental));
				this.RaisePropertyChanged ("WhiteTime");
				this.RaisePropertyChanged ("WhiteTimeStr");
				this.IsWhiteMove = false;
				this.IsBlackMove = true;
				this.StartTimer ();
			}
		}

		public ICommand ResetTimeCommand {
			get {
				if (this.resetTimeCommand == null) {
					this.resetTimeCommand = new RelayCommand (this.OnResetTime);
				}

				return this.resetTimeCommand;
			}
		}

		public ICommand StartTimeCommand {
			get {
				if (this.startTimeCommand == null) {
					this.startTimeCommand = new RelayCommand (this.StartTimer);
				}

				return this.startTimeCommand;
			}
		}

		public ICommand PauseTimeCommand {
			get {
				if (this.pauseTimeCommand == null) {
					this.pauseTimeCommand = new RelayCommand (this.OnPauseTimer);
				}

				return this.pauseTimeCommand;
			}
		}

		private void OnPauseTimer ()
		{
			if (this.IsRunning)
			{
				this.isRunning = false;
				this.RaisePropertyChanged ("IsRunning");
			}
		}

		public bool IsRunning {
			get {
				return this.isRunning;
			}
		}

		private void OnResetTime ()
		{
			this.isRunning = false;
			this.whiteTime = appSettings.WhiteTime;
			this.blackTime = appSettings.BlackTime;
			this.IsWhiteMove = true;
			this.IsBlackMove = false;
			this.RaisePropertyChanged ("BlackTime");
			this.RaisePropertyChanged ("WhiteTime");
			this.RaisePropertyChanged ("WhiteTimeStr");
			this.RaisePropertyChanged ("IsRunning");
			this.RaisePropertyChanged ("BlackTimeElapsed");
			this.RaisePropertyChanged ("WhiteTimeElapsed");
			this.RaisePropertyChanged ("IsIncrementalEnabled");
			this.RaisePropertyChanged ("Incremental");
			this.RaisePropertyChanged ("HasIncrement");
		}

		public TimeSpan WhiteTime {
			get {
				return this.whiteTime;
			}
		}

		public TimeSpan BlackTime {
			get {
				return this.blackTime;
			}
		}

		public int Incremental {
			get {
				return appSettings.Incremental;
			}
		}

		public bool HasIncremental {
			get {
				return this.Incremental > 0;
			}
		}

		public void SetWhiteTime (TimeSpan timeSpan)
		{
			this.whiteTime = timeSpan;
		}

		public void SetBlackTime (TimeSpan timeSpan)
		{
			this.blackTime = timeSpan;
		}

		public bool WhiteTimeElapsed {
			get {
				return this.whiteTime.TotalMilliseconds <= 0;
			}
		}

		public bool BlackTimeElapsed {
			get {
				return this.blackTime.TotalMilliseconds <= 0;
			}
		}

		public bool IsIncrementalEnabled {
			get {
				return this.Incremental > 0;
			}
		}
	}
}

