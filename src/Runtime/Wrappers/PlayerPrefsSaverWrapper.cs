using Racer.EzSaverLite.Static;

namespace Racer.EzSaverLite.Wrappers
{
    /// <summary>
    /// Wrapper class responsible for managing PlayerPrefs save operations.
    /// </summary>
    internal class PlayerPrefsSaverWrapper : ISaver
    {
        public void SaveInt(string key, int value) => PlayerPrefsSaver.SaveInt(key, value);
        public void SaveFloat(string key, float value) => PlayerPrefsSaver.SaveFloat(key, value);
        public void SaveString(string key, string value) => PlayerPrefsSaver.SaveString(key, value);
        public void SaveBool(string key, bool value) => PlayerPrefsSaver.SaveBool(key, value);
        public int GetInt(string key, int defaultValue) => PlayerPrefsSaver.GetInt(key, defaultValue);
        public float GetFloat(string key, float defaultValue) => PlayerPrefsSaver.GetFloat(key, defaultValue);
        public string GetString(string key, string defaultValue) => PlayerPrefsSaver.GetString(key, defaultValue);
        public bool GetBool(string key, bool defaultValue) => PlayerPrefsSaver.GetBool(key, defaultValue);
        public bool Contains(string key) => PlayerPrefsSaver.Contains(key);
        public void Clear(string key) => PlayerPrefsSaver.Clear(key);
        public void ClearAll() => PlayerPrefsSaver.ClearAll();
    }
}