using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Runtime;
using the_chess_clock.Droid.Renderers;

[assembly: ExportRenderer(typeof(TimePicker), typeof(CustomTimePickerRenderer))]
namespace the_chess_clock.Droid.Renderers
{
	public class CustomTimePickerRenderer : ViewRenderer<Xamarin.Forms.TimePicker, Android.Widget.EditText>, TimePickerDialog.IOnTimeSetListener, IJavaObject, IDisposable
	{
		private TimePickerDialog dialog = null;
		private TimePicker ctrl;

		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			this.ctrl = e.NewElement as TimePicker;
			this.SetNativeControl(new Android.Widget.EditText(Forms.Context));
			this.Control.Click += Control_Click;
			this.Control.Text = ctrl.Time.ToString(@"hh\:mm");
			this.Control.KeyListener = null;
			this.Control.FocusChange += Control_FocusChange;
		}

		void Control_FocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
		{
			if (e.HasFocus)
				ShowTimePicker();
		}

		void Control_Click(object sender, EventArgs e)
		{
			ShowTimePicker();
		}

		private void ShowTimePicker()
		{
			if (dialog == null)
			{
				dialog = new TimePickerDialog(Forms.Context, this, this.ctrl.Time.Hours, this.ctrl.Time.Minutes, true);
			}

			dialog.Show();
		}

		public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
		{
			var time = new TimeSpan(hourOfDay, minute, 0);
			this.Element.SetValue(TimePicker.TimeProperty, time);

			this.Control.Text = time.ToString(@"hh\:mm");
		}
	}

}

