using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace the_chess_clock_wp.ViewModel
{
    public class ClockViewModel : MainViewModel
    {
        private bool isWhiteMove;
        private bool isBlackMove;
        private TimeSpan whiteTime;
        private TimeSpan blackTime;
        private ICommand whiteMoveCommand;
        private ICommand blackMoveCommand;

        private Stopwatch _watch; 
        private DispatcherTimer timer;

        public ClockViewModel()
        {
            this.whiteTime = TimeSpan.FromMinutes(5);
            this.blackTime = TimeSpan.FromMinutes(5);
            this.IsWhiteMove = true;
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
                return this.whiteMoveCommand ?? new RelayCommand(this.OnWhiteMove);
            }
        }

        public ICommand BlackMoveCommand
        {
            get
            {
                return this.blackMoveCommand ?? new RelayCommand(this.OnBlackMove);
            }
        }

        private void OnBlackMove()
        {
            if (this.IsBlackMove)
            {
                this.IsBlackMove = false;
                this.IsWhiteMove = true;
            }
        }

        private void OnWhiteMove()
        {
            if (this.IsWhiteMove)
            {
                this.IsWhiteMove = false;
                this.IsBlackMove = true;
            }
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

        public int IncrementalSec { get; set; }

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
