using GUI.Configuration;
using System;
using System.IO;

namespace GUI
{
    public static class Global
    {
        public static readonly string AppPath
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WKSC_Scanner");
        public static readonly string ConfigPath
            = Path.Combine(AppPath, "config.json");
        public static readonly string OutputPath
            = Path.Combine(AppPath, "out");
    }


    public static class AppConfig
    {
        private static Config _lazyConfig = ConfigLoader.Load();

        static AppConfig()
        {
        }

        public static Config Config
        {
            get => _lazyConfig;
            internal set
            {
                _lazyConfig = value ?? throw new ArgumentNullException(nameof(value));
                ConfigChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static event EventHandler ConfigChanged;
    }
}
