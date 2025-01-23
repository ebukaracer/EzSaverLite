using Racer.EzSaverLite.Wrappers;

namespace Racer.EzSaverLite.Core
{
    /// <summary>
    /// Static class responsible for managing the current saver instance.
    /// <remarks>
    /// Use this class to access the current saver instance for various save operations.
    /// </remarks>
    /// </summary>
    public static class SaverManager
    {
        private static ISaver _saver;

        /// <summary>
        /// Gets the current saver instance. If the saver is not initialized, it initializes it automatically.
        /// </summary>
        public static ISaver Saver
        {
            get
            {
                if (_saver is null)
                    Initialize();

                return _saver;
            }
        }

        /// <summary>
        /// Initializes a custom saver based on the current build platform.
        /// </summary>
        private static void Initialize()
        {
#if !UNITY_EDITOR && UNITY_WEBGL && USE_LSS
            _saver = new LocalStorageSaverWrapper();
#else
            _saver = new PlayerPrefsSaverWrapper();
#endif
        }
    }
}