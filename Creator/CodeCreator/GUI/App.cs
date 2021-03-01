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

        public static Config Config = ConfigLoader.Load();
    }
}
