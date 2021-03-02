using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Configuration
{
    public class ConfigLoader
    {


        public static void Save(Config cfg)
        {
            try
            {
                var json = JsonConvert.SerializeObject(cfg);
                File.WriteAllText(AppConfig.ConfigPath, json);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public static Config Load()
        {
            try
            {
                var file = File.ReadAllText(AppConfig.ConfigPath);


                return JsonConvert.DeserializeObject<Config>(file) 
                    ?? new Config();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
