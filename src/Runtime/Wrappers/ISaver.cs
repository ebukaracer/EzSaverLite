namespace Racer.EzSaverLite.Wrappers
{
    public interface ISaver
    {
        #region Save

        void SaveInt(string key, int value);
        void SaveFloat(string key, float value);
        void SaveString(string key, string value);
        void SaveBool(string key, bool value);

        #endregion

        #region Get

        int GetInt(string key, int defaultValue = default);
        float GetFloat(string key, float defaultValue = default);
        string GetString(string key, string defaultValue = default);
        bool GetBool(string key, bool defaultValue = default);

        #endregion

        #region Modify

        bool Contains(string key);
        void Clear(string key);
        void ClearAll();

        #endregion
    }
}