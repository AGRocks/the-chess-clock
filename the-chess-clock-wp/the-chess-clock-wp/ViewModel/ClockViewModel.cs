using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Windows.Input;
using the_chess_clock_wp.Common;
using Windows.UI.Xaml;

namespace the_chess_clock_wp.ViewModel
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

        private Stopwatch watch;
        private DispatcherTimer timer;
        private SettingsViewModel appSettings;

        public ClockViewModel()
        {
            this.appSettings = new SettingsViewModel();
            this.whiteTime = TimeSpan.FromMinutes(5);
            this.blackTime = TimeSpan.FromMinutes(5);
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += TimerTick;
            this.IsWhiteMove = true;
        }

        private void TimerTick(object sender, object e)
        {
            if (this.IsWhiteMove)
            {
                this.whiteTime = this.whiteTime.Subtract(TimeSpan.FromSeconds(1));
                this.RaisePropertyChanged("WhiteTime");
                this.RaisePropertyChanged("WhiteTimeStr");
                return;
            }

            if (this.isBlackMove)
            {
                this.blackTime = this.blackTime.Subtract(TimeSpan.FromSeconds(1));
                this.RaisePropertyChanged("BlackTime");
                return;
            }
        }

        public bool IsWhiteMove
        {
            get
            {
                return this.isWhiteMove;
            }

            set
            {
                this.isWhiteMove = value;
                this.RaisePropertyChanged("IsWhiteMove");
            }
        }

        public bool IsBlackMove
        {
            get
            {
                return this.isBlackMove;
            }

            set
            {
                this.isBlackMove = value;
                this.RaisePropertyChanged("IsBlackMove");
            }
        }

        public ICommand WhiteMoveCommand
        {
            get
            {
                if (this.whiteMoveCommand == null)
                {
                    this.whiteMoveCommand = new RelayCommand(this.OnWhiteMove);
                }

                return this.whiteMoveCommand;
            }
        }

        public ICommand BlackMoveCommand
        {
            get
            {
                if (this.blackMoveCommand == null)
                {
                    this.blackMoveCommand = new RelayCommand(this.OnBlackMove);
                }

                return this.blackMoveCommand;
            }
        }

        private void OnBlackMove()
        {
            if (this.IsBlackMove)
            {
                this.IsBlackMove = false;
                this.IsWhiteMove = true;
                this.StartTimer();
            }
        }

        private void StartTimer()
        {
            if (!this.timer.IsEnabled)
            {
                this.timer.Start();
                this.RaisePropertyChanged("IsRunning");
            }
        }

        private void OnWhiteMove()
        {
            if (this.IsWhiteMove)
            {
                this.IsWhiteMove = false;
                this.IsBlackMove = true;
                this.StartTimer();
            }
        }

        public ICommand ResetTimeCommand
        {
            get
            {
                if (this.resetTimeCommand == null)
                {
                    this.resetTimeCommand = new RelayCommand(this.OnResetTime);
                }

                return this.resetTimeCommand;
            }
        }

        public ICommand StartTimeCommand
        {
            get
            {
                if (this.startTimeCommand == null)
                {
                    this.startTimeCommand = new RelayCommand(this.OnStartTime);
                }

                return this.startTimeCommand;
            }
        }

        private void OnStartTime()
        {
            this.timer.Start();
            this.RaisePropertyChanged("IsRunning");
        }

        public bool IsRunning
        {
            get
            {
                return this.timer.IsEnabled;
            }
        }

        private void OnResetTime()
        {
            this.timer.Stop();
            this.whiteTime = appSettings.WhiteTime;
            this.blackTime = appSettings.BlackTime;
            this.IsWhiteMove = true;
            this.IsBlackMove = false;
            this.RaisePropertyChanged("BlackTime");
            this.RaisePropertyChanged("WhiteTime");
            this.RaisePropertyChanged("WhiteTimeStr");
            this.RaisePropertyChanged("IsRunning");
        }

        public TimeSpan WhiteTime
        {
            get
            {
                return this.whiteTime;
            }
        }

        public string WhiteTimeStr
        {
            get
            {
                return this.WhiteTime.ToString();
            }
        }

        public TimeSpan BlackTime
        {
            get
            {
                return this.blackTime;
            }
        }

        public int Incremental
        {
            get 
            {
                return appSettings.Incremental;
            }
        }

        public void SetWhiteTime(TimeSpan timeSpan)
        {
            this.whiteTime = timeSpan;
        }

        public void SetBlackTime(TimeSpan timeSpan)
        {
            this.blackTime = timeSpan;
        }
    }
}
