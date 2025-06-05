#if USE_LSS
using Racer.EzSaverLite.Static;
using UnityEngine;


namespace Racer.EzSaverLite.Wrappers
{
    /// <summary>
    /// Wrapper class responsible for managing LocalStorage save operations in WebGL builds.
    /// </summary>
    internal class LocalStorageSaverWrapper : ISaver
    {
        private readonly string _prefix;

        public LocalStorageSaverWrapper(string prefix = "")
        {
            _prefix = string.IsNullOrEmpty(prefix) ? Application.productName : prefix;
        }

        public void SaveInt(string key, int value) => LocalStorageSaver.SaveInt(ModifiedKey(key), value);

        public void SaveFloat(string key, float value) => LocalStorageSaver.SaveFloat(ModifiedKey(key), value);

        public void SaveString(string key, string value) => LocalStorageSaver.SaveString(ModifiedKey(key), value);

        public void SaveBool(string key, bool value) => LocalStorageSaver.SaveBool(ModifiedKey(key), value);

        public int GetInt(string key, int defaultValue) => LocalStorageSaver.GetInt(ModifiedKey(key), defaultValue);

        public float GetFloat(string key, float defaultValue) =>
            LocalStorageSaver.GetFloat(ModifiedKey(key), defaultValue);

        public string GetString(string key, string defaultValue) =>
            LocalStorageSaver.GetString(ModifiedKey(key), defaultValue);

        public bool GetBool(string key, bool defaultValue) => LocalStorageSaver.GetBool(ModifiedKey(key), defaultValue);

        public bool Contains(string key) => LocalStorageSaver.Contains(ModifiedKey(key));

        public void Clear(string key) => LocalStorageSaver.Clear(ModifiedKey(key));

        public void ClearAll() => LocalStorageSaver.ClearAll();

        private string ModifiedKey(string key) => $"{_prefix}_{key}";
    }
}
#endif