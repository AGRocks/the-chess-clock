using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the_chess_clock_wp.Common;

namespace the_chess_clock_wp.WP8.Common
{
    public class AppSettings : ISetttingsProvider
    {
        private IsolatedStorageSettings isolatedStore;
        public AppSettings()
        {
            try
            {
                isolatedStore = IsolatedStorageSettings.ApplicationSettings;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e.ToString());
            }
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetValue(string Key, object value)
        {
            if (isolatedStore.Contains(Key))
            {
                if (isolatedStore[Key] != value)
                {
                    isolatedStore[Key] = value;
                }
            }
            else
            {
                isolatedStore.Add(Key, value);
            }
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            if (isolatedStore.Contains(Key))
            {
                value = (T)isolatedStore[Key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }


        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            isolatedStore.Save();
        }
    }
}
