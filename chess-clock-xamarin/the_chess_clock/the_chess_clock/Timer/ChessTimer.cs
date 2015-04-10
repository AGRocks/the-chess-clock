using System;
using System.Threading;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace the_chess_clock
{
	internal delegate void ChessTimerCallback (object state);

	internal sealed class ChessTimer : IDisposable
	{
		private ChessTimerCallback callback;
		private CancellationTokenSource cancellationTokenSource;
		private readonly int period = 1;
		private bool isEnabled;

		internal ChessTimer (ChessTimerCallback callback)
		{
			this.callback = callback;
		}

		public void Start ()
		{
			this.cancellationTokenSource = new CancellationTokenSource ();
			this.isEnabled = true;
			Task.Delay (1, this.cancellationTokenSource.Token).ContinueWith (async (t, s) => {
				var tuple = (Tuple<ChessTimerCallback, object>)s;

				while (true) {
					if (this.cancellationTokenSource.Token.IsCancellationRequested)
						break;
					Task.Run (() => tuple.Item1 (tuple.Item2));
					await Task.Delay (period);
				}
			}, Tuple.Create (callback, this), CancellationToken.None,
				TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
				TaskScheduler.Default);
		}

		public void Stop ()
		{
			if (cancellationTokenSource.Token.CanBeCanceled) {
				cancellationTokenSource.Cancel (false);
				this.isEnabled = false;
			}
		}

		public new void Dispose ()
		{
			this.Stop ();
		}

		public bool IsEnabled {
			get {
				return this.isEnabled;
			}
		}
	}
}

