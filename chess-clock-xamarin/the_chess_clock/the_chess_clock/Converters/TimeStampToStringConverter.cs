using System;
using Xamarin.Forms;

namespace the_chess_clock
{
	public class TimeStampToStringConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null && value is TimeSpan) {
				return ((TimeSpan)value).ToString ();
			}

			return string.Empty;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

