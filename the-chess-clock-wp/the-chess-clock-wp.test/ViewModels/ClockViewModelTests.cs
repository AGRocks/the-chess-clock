using NUnit.Framework;
using System;
using the_chess_clock_wp.ViewModel;

namespace the_chess_clock_wp.test.ViewModels
{
    [TestFixture]
    public class ClockViewModelTests
    {
        [Test]
        public void WhetherWhiteMoveEnablesBlackMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsWhiteMove = true;

            viewModel.WhiteMoveCommand.Execute(null);

            Assert.That(viewModel.IsBlackMove);
        }

        [Test]
        public void WhetherBlackMoveEnablesWhiteMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsBlackMove = true;

            viewModel.BlackMoveCommand.Execute(null);

            Assert.That(viewModel.IsWhiteMove);
        }

        [Test]
        public void WhetherTimerChangesOnWhiteMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsBlackMove = true;
            var oneMinute = new TimeSpan(0, 1, 0);
            viewModel.SetWhiteTime(oneMinute);

            viewModel.BlackMoveCommand.Execute(null);

            Assert.That(viewModel.WhiteTime < oneMinute);
        }

        [Test]
        public void WhehterTimerChangesOnBlackMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsWhiteMove = true;
            var oneMinute = new TimeSpan(0, 1, 0);
            viewModel.SetBlackTime(oneMinute);

            viewModel.WhiteMoveCommand.Execute(null);

            Assert.That(viewModel.BlackTime < oneMinute);
        }
    }
}
