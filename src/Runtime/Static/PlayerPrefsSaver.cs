using UnityEngine;

namespace Racer.EzSaverLite.Static
{
    /// <summary>
    /// Static class responsible for managing PlayerPrefs save operations.
    /// </summary>
    public static class PlayerPrefsSaver
    {
        #region Save

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static void SaveBool(string key, bool value)
        {
            if (value) PlayerPrefs.SetInt(key, 1);
            else PlayerPrefs.SetInt(key, -1);
        }

        #endregion

        #region Retrieve

        public static int GetInt(string key, int defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static float GetFloat(string key, float defaultValue = default)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static string GetString(string key, string defaultValue = default)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static bool GetBool(string key, bool defaultValue = default)
        {
            if (PlayerPrefs.HasKey(key))
                return PlayerPrefs.GetInt(key) == 1;

            return defaultValue;
        }

        #endregion

        #region Modify

        public static bool Contains(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void Clear(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion
    }
}