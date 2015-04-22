using System;
using Xamarin.Forms;

namespace the_chess_clock
{
	public class TimeElapsedToBackgroundConverter : IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (bool)value ? Color.FromHex ("#f58462") : Color.FromHex ("#" + (parameter ?? "ffffff"));
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

