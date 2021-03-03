using GUI.Configuration;
using System;
using System.IO;

namespace GUI
{
    public static class AppConfig
    {
        public static readonly string AppPath
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WKSC_Scanner");
        public static readonly string ConfigPath
            = Path.Combine(AppPath, "config.json");
        private static Config _config = ConfigLoader.Load();

        public static Config Config
        {
            get => _config;
            private set => _config = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static event EventHandler ConfigChanged;

        public static void SetConfig(Config newcfg)
        {
            _config = newcfg ?? throw new ArgumentNullException(nameof(newcfg));
            ConfigChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
