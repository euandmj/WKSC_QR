using Data.CSV;
using System;
using System.Collections.Generic;
using System.IO;
using GUI.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static event EventHandler NewEvent;

        public static void SetConfig(Config newcfg)
        {
            if (newcfg == null) throw new ArgumentNullException(nameof(newcfg));

            _config = newcfg;
            NewEvent?.Invoke(null, EventArgs.Empty);
        }
    }
}
