#if USE_LSS
using System.Globalization;
using System.Runtime.InteropServices;

namespace Racer.EzSaverLite.Static
{
    /// <summary>
    /// Static class responsible for managing LocalStorage save operations in WebGL builds.
    /// <remarks>
    /// Ensure the required '.jslib' file is present in 'Plugins' folder, otherwise, simply 
    /// use the 'Import WebGL Save Plugin?' context menu option to import the plugin.
    /// </remarks>
    /// </summary>
    internal static class LocalStorageSaver
    {
        #region JS Methods

        // Import JavaScript methods
        [DllImport("__Internal")]
        private static extern void setLocalStorage(string key, string value);

        [DllImport("__Internal")]
        private static extern string getLocalStorage(string key, string defaultValue);

        [DllImport("__Internal")]
        private static extern void removeLocalStorage(string key);

        [DllImport("__Internal")]
        private static extern void clearLocalStorage();

        #endregion

        #region C# Methods

        // Define C# methods that call the imported JavaScript methods

        #region Save

        public static void SaveInt(string key, int value)
        {
            setLocalStorage(key, value.ToString());
        }

        public static void SaveFloat(string key, float value)
        {
            setLocalStorage(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public static void SaveString(string key, string value)
        {
            setLocalStorage(key, value);
        }

        public static void SaveBool(string key, bool value)
        {
            setLocalStorage(key, value.ToString());
        }

        #endregion

        private static string Get(string key, string defaultValue)
        {
            return getLocalStorage(key, defaultValue);
        }

        #region Get

        public static int GetInt(string key, int defaultValue)
        {
            return int.Parse(Get(key, defaultValue.ToString()));
        }

        public static float GetFloat(string key, float defaultValue)
        {
            return float.Parse(Get(key, defaultValue.ToString(CultureInfo.InvariantCulture)));
        }

        public static string GetString(string key, string defaultValue)
        {
            return Get(key, defaultValue);
        }

        public static bool GetBool(string key, bool defaultValue)
        {
            return bool.Parse(Get(key, defaultValue.ToString()));
        }

        #endregion

        #region Modify

        public static bool Contains(string key)
        {
            return Get(key, string.Empty) != string.Empty;
        }

        public static void Clear(string key)
        {
            removeLocalStorage(key);
        }

        public static void ClearAll()
        {
            clearLocalStorage();
        }

        #endregion

        #endregion
    }
}
#endif