using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace the_chess_clock_wp.Common
{
    public class AppSettings : ISetttingsProvider
    {
        ApplicationDataContainer appData;

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            appData = ApplicationData.Current.LocalSettings;
        }

        public void SetValue(string key, object value)
        {
            appData.Values[key] = value;
        }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (appData.Values.ContainsKey(key))
            {
                value = (T)appData.Values[key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }
    }
}
