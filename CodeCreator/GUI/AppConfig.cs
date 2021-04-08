using GUI.Configuration;
using System;
using System.Diagnostics;
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
        private static readonly FileSystemWatcher _fsWatcher;

        static AppConfig()
        {

            try
            {
                _fsWatcher = new FileSystemWatcher(Path.GetDirectoryName(_lazyConfig.SpreadsheetFile))
                {
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size,
                    Filter = "*.csv"
                };

                _fsWatcher.Changed += (s, e) =>
                {
                    ConfigChanged?.Invoke(s, e);
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("failed creating file watcher - " + ex.Message);
            }
        }

        public static Config Config
        {
            get => _lazyConfig;
            internal set
            {
                _lazyConfig = value ?? throw new ArgumentNullException(nameof(value));
                _fsWatcher.Path = Path.GetDirectoryName(value.SpreadsheetFile);
                ConfigChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static event EventHandler ConfigChanged;
    }
}
