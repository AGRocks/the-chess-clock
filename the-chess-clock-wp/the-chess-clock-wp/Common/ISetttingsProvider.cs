using System;

namespace the_chess_clock_wp.Common
{
    public interface ISetttingsProvider
    {
        void SetValue(string key, object value);
        T GetValueOrDefault<T>(string key, T defaultValue);
    }
}
