using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using the_chess_clock_wp.ViewModel;

namespace the_chess_clock_wp.test.ViewModels
{
    [TestClass]
    public class ClockViewModelTests
    {
        [TestMethod]
        public void WhetherWhiteMoveEnablesBlackMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsWhiteMove = true;

            viewModel.WhiteMoveCommand.Execute(null);

            Assert.IsTrue(viewModel.IsBlackMove);
        }

        [TestMethod]
        public void WhetherBlackMoveEnablesWhiteMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsBlackMove = true;

            viewModel.BlackMoveCommand.Execute(null);

            Assert.IsTrue(viewModel.IsWhiteMove);
        }

        [TestMethod]
        public void WhetherTimerChangesOnWhiteMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsBlackMove = true;
            var oneMinute = new TimeSpan(0, 1, 0);
            viewModel.SetWhiteTime(oneMinute);

            viewModel.BlackMoveCommand.Execute(null);
            
            Assert.IsTrue(viewModel.WhiteTime < oneMinute);
        }

        [TestMethod]
        public void WhehterTimerChangesOnBlackMove()
        {
            var viewModel = new ClockViewModel();
            viewModel.IsWhiteMove = true;
            var oneMinute = new TimeSpan(0, 1, 0);
            viewModel.SetBlackTime(oneMinute);

            viewModel.WhiteMoveCommand.Execute(null);

            Assert.IsTrue(viewModel.BlackTime < oneMinute);
        }
    }
}
